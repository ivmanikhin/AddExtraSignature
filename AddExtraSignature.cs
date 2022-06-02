// Decompiled with JetBrains decompiler
// Type: Ascon.Pilot.SDK.GraphicLayerSample.GraphicLayerSample
// Assembly: Ascon.Pilot.SDK.GraphicLayerSample.ext2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 187B3BB9-3768-4B7C-861E-6A56C03BF53E
// Assembly location: D:\Projects\Pilot-ICE\SDK\b396a650-48de-48bb-bf68-8ed251a97fbe\Ascon.Pilot.SDK.GraphicLayerSample.ext2.dll

using Ascon.Pilot.SDK.GraphicLayerSample.Properties;
using Ascon.Pilot.SDK.Menu;
using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Serialization;
using Ascon.Pilot.SDK.ObjectsSample;
using System.Xml.Linq;
using GraphicLayerSample.Properties;

namespace Ascon.Pilot.SDK.GraphicLayerSample
{
      
    [Export(typeof(IMenu<XpsRenderClickPointContext>))]
    public class AddExtraSignature : IMenu<XpsRenderClickPointContext>
    {
        private readonly IObjectsRepository _repository;
        private DataObjectWrapper _selected;
        private readonly string dec_separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        private readonly IObjectModifier _modifier;
        private readonly IPerson _currentPerson;
        private string _filePath = string.Empty;
        private bool _includeStamp;
        private bool xpsIsSigned = false;
        private double _xOffset;
        private double _yOffset;
        private double _scaleXY;
        private double _angle;
        private int _pageNumber;
        //private int signatureNumber = 0;
        private VerticalAlignment _verticalAlignment;
        private HorizontalAlignment _horizontalAlignment;
        private bool gotAccess = false;
        bool notFrozen = false;
        private AccessLevel _accessLevel = AccessLevel.None;
        private const string AddExtraSignatureMenuItem = "AddExtraSignatureMenuItem";


        [ImportingConstructor]
        public AddExtraSignature(IObjectModifier modifier, IObjectsRepository repository)
        {
            _modifier = modifier;
            _currentPerson = repository.GetCurrentPerson();
            _repository = repository;  

        }


        public void Build(IMenuBuilder builder, XpsRenderClickPointContext context)
        //создание пунктов меню: "Перенести подпись сюда" и "Перенести сюда и повернуть":
        {
            //запрос прав на согласование документа:
            _selected = new DataObjectWrapper(context.DataObject, _repository);
            _accessLevel = GetMyAccessLevel(_selected);
            gotAccess = _accessLevel.ToString().Contains("Agrement") |
                        _accessLevel.ToString().Contains("Agreement") |
                        _accessLevel.ToString().Contains("Full");
            // проверка, не заморожен ли документ
            notFrozen = !(_selected.StateInfo.State.ToString().Contains("Frozen"));
            // проверка, подписал ли подписант
            xpsIsSigned = XPSSignedByCyrrentUser(context.DataObject);


            builder.AddItem(AddExtraSignatureMenuItem, 0)
                   .WithHeader(Resources.AddExtraSignatureMenuItem)
                   .WithIsEnabled(gotAccess & notFrozen & xpsIsSigned); //пункт меню активен, если есть право согласовывать, есть электронная подпись текущего пользователя

        }

        public void OnMenuItemClick(string name, XpsRenderClickPointContext context)
        {

            if (name == AddExtraSignatureMenuItem)
            {
                CheckSettings(); //чтение натсроек подписи
                _pageNumber = context.PageNumber + 1; //задание номера страницы
                _xOffset = (context.ClickPoint.X - 10 / _scaleXY) * 25.4 / 96; //установка координат подписи в точку клика мышом
                _yOffset = (context.ClickPoint.Y - 4 / _scaleXY) * 25.4 / 96;
                if (SignatureIsOnXPS(context.DataObject))
                    AddRastrToXPS(context.DataObject);
                else
                    DrawMainSignature(context.DataObject);
            }
                       
        }

        private void CheckSettings()
        {
            _filePath = Settings.Default.Path;
            _includeStamp = Settings.Default.IncludeStamp;
            if (!double.TryParse(Settings.Default.Scale.Replace(".", dec_separator).Replace(",", dec_separator), out _scaleXY)) //если трайпарс не смог,
                _scaleXY = 1;                                                                                                   //масштаб равен 1
            double.TryParse(Settings.Default.Angle, out _angle);
        }



        //private int CountSignaturesOfCurrentUser(IDataObject dataObject)
        //{
        //    signatureNumber = 0;    
        //    foreach (IFile file in dataObject.Files)
        //    {
        //        if (file.Name.Contains("PILOT_GRAPHIC_LAYER_ELEMENT_" + ToGuid(_currentPerson.Id)))
        //            signatureNumber ++;
        //    }
        //    return signatureNumber;
        //}

        // Рисование дополнительной подписи текущего пользователя

        private void AddRastrToXPS(IDataObject dataObject)
        {

            var elementId = Guid.NewGuid(); // рандомный GUID
            if (string.IsNullOrEmpty(_filePath))
                return;
            IObjectBuilder objectBuilder = _modifier.Edit(dataObject);
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                int position = _currentPerson.MainPosition.Position;
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                MemoryStream memoryStream1 = new MemoryStream(buffer);
                Point scale = new Point(_scaleXY, _scaleXY);
                string name = "PILOT_GRAPHIC_LAYER_ELEMENT_" + elementId + "_" + position; //имя файла с записью свойств картинки
                                                                                           //ПРИВЯЗАНО К ЧЕЛОВЕКУ В ВИДЕ _currentPerson.MainPosition.Position в конце имени файла
                GraphicLayerElement o = GraphicLayerElementCreator.Create(_xOffset, _yOffset, scale, _angle, position, _verticalAlignment, _horizontalAlignment, "bitmap", elementId, _pageNumber, true);
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    new XmlSerializer(typeof(GraphicLayerElement)).Serialize(memoryStream2, o);
                    objectBuilder.AddFile(name, memoryStream2, DateTime.Now, DateTime.Now, DateTime.Now); //создание записи о расположении картинки на листе
                    objectBuilder.AddFile("PILOT_CONTENT_GRAPHIC_LAYER_ELEMENT_" + o.ContentId, memoryStream1, DateTime.Now, DateTime.Now, DateTime.Now); //создание файла PNG. НЕ СОДЕРЖИТ ПРИВЯЗКУ К ЧЕЛОВЕКУ.
                                                                                                                                                          //CONTENT ID - РАНДОМНЫЙ GUID
                }
                _modifier.Apply();
            }

        }

        // Рисование главной подписи текущего пользователя
        private void DrawMainSignature(IDataObject dataObject)
        {
            if (string.IsNullOrEmpty(_filePath))
                return;
            IObjectBuilder objectBuilder = _modifier.Edit(dataObject);
            using (FileStream fileStream = File.Open(_filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                int position = _currentPerson.MainPosition.Position;
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                MemoryStream memoryStream1 = new MemoryStream(buffer);
                Point scale = new Point(_scaleXY, _scaleXY);
                string name = "PILOT_GRAPHIC_LAYER_ELEMENT_" + ToGuid(_currentPerson.Id); //имя файла с записью свойств картинки
                                                                                                             //ПРИВЯЗАНО К ЧЕЛОВЕКУ В ВИДЕ GUID C НУЛЯМИ (В ОСНОВНОМ)
                GraphicLayerElement o = GraphicLayerElementCreator.Create(_xOffset, _yOffset, scale, _angle, position, _verticalAlignment, _horizontalAlignment, "bitmap", ToGuid(_currentPerson.Id), _pageNumber, true);
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    new XmlSerializer(typeof(GraphicLayerElement)).Serialize(memoryStream2, o);
                    objectBuilder.AddFile(name, memoryStream2, DateTime.Now, DateTime.Now, DateTime.Now); //создание записи о расположении картинки на листе
                    objectBuilder.AddFile("PILOT_CONTENT_GRAPHIC_LAYER_ELEMENT_" + o.ContentId, memoryStream1, DateTime.Now, DateTime.Now, DateTime.Now); //создание файла PNG. НЕ СОДЕРЖИТ ПРИВЯЗКУ К ЧЕЛОВЕКУ.
                                                                                                                                                          //CONTENT ID - РАНДОМНЫЙ GUID
                }
                _modifier.Apply();
            }
        }


        public static Guid ToGuid(int value)
        {
            byte[] b = new byte[16];
            BitConverter.GetBytes(value).CopyTo(b, 0);
            return new Guid(b);
        }


        private bool XPSSignedByCyrrentUser(IDataObject dataObject)
        {
            foreach (IFile file in dataObject.Files)
            {
                if (file.CreatorId().Equals(_currentPerson.Id) & file.Name.Equals("PilotDigitalSignature"))
                    return true;
            }
            return false;
        }

        private bool SignatureIsOnXPS(IDataObject dataObject)
        {
            foreach (IFile file in dataObject.Files)
            {
                if (file.Name.Equals("PILOT_GRAPHIC_LAYER_ELEMENT_" + ToGuid(_currentPerson.Id)))
                    return true;
            }
            return false;
        }


        private AccessLevel GetMyAccessLevel(DataObjectWrapper element)
        {
            var currentAccesLevel = AccessLevel.None;
            var person = _repository.GetCurrentPerson();
            foreach (var position in person.Positions)
            {
                currentAccesLevel = currentAccesLevel | GetAccessLevel(element, position.Position);
            }

            return currentAccesLevel;
        }

        private AccessLevel GetAccessLevel(DataObjectWrapper element, int positonId)
        {
            var currentAccesLevel = AccessLevel.None;
            var orgUnits = _repository.GetOrganisationUnits().ToDictionary(k => k.Id);
            var accesses = GetAccessRecordsForPosition(element, positonId, orgUnits);
            foreach (var source in accesses)
            {
                currentAccesLevel = currentAccesLevel | source.Access.AccessLevel;
            }
            return currentAccesLevel;
        }

        private IEnumerable<AccessRecordWrapper> GetAccessRecordsForPosition(DataObjectWrapper obj, int positionId, IDictionary<int, IOrganisationUnit> organisationUnits)
        {
            return obj.Access.Where(x => BelongsTo(positionId, x.OrgUnitId, organisationUnits));
        }

        public static bool BelongsTo(int position, int organisationUnit, IDictionary<int, IOrganisationUnit> organisationUnits)
        {
            Stack<int> units = new Stack<int>();
            units.Push(organisationUnit);
            while (units.Any())
            {
                var unitId = units.Pop();
                if (position == unitId)
                    return true;

                IOrganisationUnit unit;
                if (organisationUnits.TryGetValue(unitId, out unit))
                {
                    foreach (var childUnitId in unit.Children)
                    {
                        units.Push(childUnitId);
                    }
                }
            }
            return false;
        }
    }
}
