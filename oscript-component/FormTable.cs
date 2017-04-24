﻿using ScriptEngine.HostedScript.Library;
using ScriptEngine.HostedScript.Library.ValueTable;
using ScriptEngine.Machine;
using ScriptEngine.Machine.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace oscriptGUI
{
    /// <summary>
    /// Элемент управляемой формы, предназначенный для отображения реквизитов формы табличных типов.
    /// </summary>
    [ContextClass("ТаблицаФормы", "FormTable")]
    class FormTable : AutoContext<FormTable>, IFormElement
    {
        private Panel _panelMainContainer;
        private Panel _panelTitleContainer;
        private Panel _panelControlContainer;
        private Label _label;
        private Control _item;

        private string _name;
        private bool _visible;
        private bool _enabled;
        private string _title;
        private IElementsContainer _parent;
        private Control _parentControl;
        private int _titleLocation;
        private IRuntimeContextInstance _thisScript;
        private string _methodName;

        private IRuntimeContextInstance _thisScriptDblClick;
        private string _methodNameDblClick;

        private IRuntimeContextInstance _scriptOnChoice;
        private string _methodOnChoice;

        private BindingSource _bindingSource;
        private DataTableProvider _dataTable;
        private ArrayImpl _columns = new ArrayImpl();

        public FormTable(Control parentCntrl)
        {

            this._name = "";
            this._visible = true;
            this._enabled = true;
            this._title = "";
            this._parent = null;
            this._parentControl = parentCntrl;

            this._methodName = "";
            this._thisScript = null;

            this._methodNameDblClick = "";
            this._thisScriptDblClick = null;

            this._methodOnChoice = "";
            this._scriptOnChoice = null;

            _bindingSource = new BindingSource();
            _dataTable = new DataTableProvider();
            _item = new DataGridView();


            //# Создаем контейнер для элемента формы
            _panelMainContainer = new Panel();
            _panelTitleContainer = new Panel();
            _panelControlContainer = new Panel();

            _panelMainContainer.Controls.Add(_panelControlContainer);
            _panelMainContainer.Controls.Add(_panelTitleContainer);

            _panelMainContainer.Dock = DockStyle.Top;
            _panelMainContainer.MinimumSize = new Size(150, 22);
            _panelMainContainer.AutoSize = true;
            _panelMainContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            _panelTitleContainer.Dock = DockStyle.Left;
            _panelTitleContainer.MinimumSize = new Size(50, 21);
            _panelTitleContainer.AutoSize = true;
            _panelTitleContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _label = new Label();
            _panelTitleContainer.Controls.Add(_label);
            _label.AutoSize = true;
            _label.Dock = DockStyle.Fill;


            //# Установка параметров панели для поля с данными
            _panelControlContainer.Dock = DockStyle.Fill;
            _panelControlContainer.MinimumSize = new Size(100, 21);
            _panelControlContainer.AutoSize = true;
            _panelControlContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            this._parentControl.Controls.Add(_panelMainContainer);
            _panelMainContainer.BringToFront();

            this.createFormFieldByType();

        }

        public override string ToString()
        {
            return "ТаблицаФормы";
        }

        /// <summary>
        /// Возвращает основной контейнер элемента (Panel на котором лежит сам контрол и декорация к нему)
        /// </summary>
        /// <returns>Control - Panel</returns>
        public Control getBaseControl()
        {
            return _panelMainContainer;
        }

        public Control getControl()
        {
            return _item;
        }

        public void setParent(IValue parent)
        {
            _parent = (IElementsContainer)parent;
        }

        private void createFormFieldByType()
        {
            //DataGridView newItem = new DataGridView();

            ((DataGridView)this._item).MinimumSize = new Size(100, 100);
            ((DataGridView)this._item).ReadOnly = true;
            ((DataGridView)this._item).AllowUserToAddRows = false;
            ((DataGridView)this._item).AllowUserToDeleteRows = false;

            ((DataGridView)this._item).AutoSize = true;
            ((DataGridView)this._item).Dock = DockStyle.Fill;
            //this._item = newItem;
            _panelControlContainer.Controls.Add(this._item);
        }

        private void setTitleLocation()
        {
            switch (this._titleLocation)
            {
                case (int)EnumTitleLocation.Auto:
                    _panelTitleContainer.Dock = DockStyle.Left;
                    break;
                case (int)EnumTitleLocation.Bottom:
                    _panelTitleContainer.Dock = DockStyle.Bottom;
                    break;
                case (int)EnumTitleLocation.Left:
                    _panelTitleContainer.Dock = DockStyle.Left;
                    break;
                case (int)EnumTitleLocation.None:
                    _panelTitleContainer.Visible = false;
                    break;
                case (int)EnumTitleLocation.Right:
                    _panelTitleContainer.Dock = DockStyle.Right;
                    break;
                case (int)EnumTitleLocation.Top:
                    _panelTitleContainer.Dock = DockStyle.Top;
                    break;
            }
        }

        /// <summary>
        /// Имя элемента
        /// </summary>
        [ContextProperty("Имя", "Name")]
        public string Name
        {
            get { return this._name; }
            set
            {
                this._parent.Items.renameElement(this._name, value);
                this._name = value;
            }
        }

        /// <summary>
        /// Управление видимостью
        /// </summary>
        [ContextProperty("Видимость", "Visible")]
        public bool Visible
        {
            get { return this._visible; }
            set
            {
                this._visible = value;
                this._panelMainContainer.Visible = this._visible;
            }
        }

        /// <summary>
        /// Управление доступностью
        /// </summary>
        [ContextProperty("Доступность", "Enabled")]
        public bool Enabled
        {
            get { return this._enabled; }
            set
            {
                this._enabled = value;
                this._panelMainContainer.Enabled = this._enabled;
            }
        }

        /// <summary>
        /// Заголовок к полю. Пустая строка означает автоматическое определение. Для отключения вывода заголовка следует установить свойство ПоложениеЗаголовка в значение Нет.
        /// </summary>
        [ContextProperty("Заголовок", "Title")]
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this._label.Text = this._title;
                if (this._label.Text.Trim() == String.Empty)
                {
                    this._label.Text = this.Name;
                }
            }
        }

        /// <summary>
        /// Определяет положение заголовка относительно поля в макете формы. 
        /// Следует заметить, что для отключения вывода заголовка следует установить это свойство в значение Нет. 
        /// Свойство Заголовок, содержащее пустую строку, означает автоматическое определение заголовка, а не ее отключение.
        /// </summary>
        /// <value>ПоложениеЗаголовкаЭлементаФормы</value>
        [ContextProperty("ПоложениеЗаголовка", "TitleLocation")]
        public int TitleLocation
        {
            get { return this._titleLocation; }
            set
            {
                this._titleLocation = value;
                this.setTitleLocation();
            }
        }


        /// <summary>
        /// Содержит ссылку на родительский элемент. <see cref="FormGroup"/>
        /// </summary>
        /// <value>ГруппаФормы, Форма</value>
        [ContextProperty("Родитель", "Parent")]
        public IValue Parent
        {
            get { return this._parent; }
        }

        //# Блок по работе с событиями

        private void runAction(IRuntimeContextInstance script, string method)
        {
            if (script == null)
            {
                return;
            }

            if (method.Trim() == String.Empty)
            {
                return;
            }

            ScriptEngine.HostedScript.Library.ReflectorContext reflector = new ScriptEngine.HostedScript.Library.ReflectorContext();
            reflector.CallMethod(script, method, null);
        }

        private void FormFieldValueChanged(object sender, EventArgs e)
        {
            runAction(this._thisScript, this._methodName);
        }

        private void FormFieldDblClick(object sender, EventArgs e)
        {
            runAction(this._thisScriptDblClick, this._methodNameDblClick);
        }


        private void FormFieldOnChoice(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                runAction(this._scriptOnChoice, this._methodOnChoice);
            }
        }

        /// <summary>
        /// Установить обработчик события
        /// Возможные события:
        /// - ПриПотереФокусаЯчейки - Обработка события изменения значения
        /// - ПриВыборе - При нажатии Enter
        /// - ПриДвойномКлике - Обработка двойного клика
        /// </summary>
        /// <param name="contex">Ссылка на скрипт в котором находится обработчик события</param>
        /// <param name="eventName">Имя обрабатываемого события.</param>
        /// <param name="methodName">Имя метода обработчика события</param>
        /// <example>
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриПотереФокусаЯчейки", "ПриПотереФокусаЯчейки");
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриДвойномКлике", "ПриДвойномКлике");
        /// Поле1.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыборе");
        /// </example>
        [ContextMethod("УстановитьДействие", "SetAction")]
        public void setAction(IRuntimeContextInstance contex, string eventName, string methodName)
        {
            if (eventName == "ПриПотереФокусаЯчейки")
            {

                this._thisScript = contex;
                this._methodName = methodName;

                ((DataGridView)_item).CurrentCellChanged -= FormFieldValueChanged;
                ((DataGridView)_item).CurrentCellChanged += FormFieldValueChanged;

            }
            else if (eventName == "ПриДвойномКлике")
            {
                ((DataGridView)_item).CellDoubleClick -= FormFieldDblClick;
                ((DataGridView)_item).CellDoubleClick += FormFieldDblClick;

                this._thisScriptDblClick = contex;
                this._methodNameDblClick = methodName;

            }
            else if (eventName == "ПриВыборе")
            {
                (_item).KeyPress -= FormFieldOnChoice;
                (_item).KeyPress += FormFieldOnChoice;

                this._scriptOnChoice = contex;
                this._methodOnChoice = methodName;
            }

        }

        /// <summary>
        /// Получает имя установленного обработчика события.
        /// </summary>
        /// <param name="eventName">Имя события</param>
        /// <returns>Имя метода обработчика события</returns>
        /// <example>
        /// Форма.УстановитьДействие(ЭтотОбъект, "ПриВыборе", "ПриВыбореЯчейки");
        /// Форма.ПолучитьДействие("ПриВыборе");
        /// // вернет: "ПриВыбореЯчейки"
        /// </example>
        [ContextMethod("ПолучитьДействие", "GetAction")]
        public string GetAction(string eventName)
        {
            if (eventName == "ПриПотереФокусаЯчейки")
            {
                return "" + this._thisScript.ToString() + ":" + this._methodName;
            }
            else if (eventName == "ПриДвойномКлике")
            {
                return "" + this._thisScriptDblClick.ToString() + ":" + this._methodNameDblClick;
            }
            else if (eventName == "ПриВыборе")
            {
                return "" + this._scriptOnChoice.ToString() + ":" + this._methodOnChoice;
            }
            return "";
            //return "GetAction: Action not supported - " + eventName;
        }

        /// <summary>
        /// Высота
        /// </summary>
        [ContextProperty("Высота", "Height")]
        public int Height
        {
            get { return _item.Height; }
            set
            {
                _panelMainContainer.Height = value;
            }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        [ContextProperty("Ширина", "Width")]
        public int Width
        {
            get { return _item.Width; }
            set { _item.Width = value; }
        }

        /// <summary>
        /// Автоматический размер
        /// </summary>
        [ContextProperty("АвтоматическийРазмер", "AutoSize")]
        public bool AutoSize
        {
            get { return _item.AutoSize; }
            set { _item.AutoSize = value; }
        }

        /// <summary>
        /// Вариант закрепления. <see cref="FormControlDockStyle"/>
        /// </summary>
        /// <value>СтильЗакрепления</value>
        [ContextProperty("Закрепление", "Dock")]
        public int Dock
        {
            get { return _item.Dock.GetHashCode(); }
            set
            {
                _panelMainContainer.Dock = (DockStyle)value;


            }
        }

        private void setData()
        {
            _bindingSource.DataSource = _dataTable.getData();
            ((DataGridView)_item).DataSource = _bindingSource;

        }

        private void getColumns()
        {
            var cols = ((DataGridView)_item).Columns;

            foreach (DataGridViewColumn col in cols)
            {
                _columns.Add(new FormTableColumn(((DataGridView)_item), _columns.Count()));
            }

        }


        /// <summary>
        /// Провайдер с данными.
        /// </summary>
        [ContextProperty("ПутьКДанным", "DataPath")]
        public DataTableProvider DataPath
        {
            get { return _dataTable; }
            set
            {
                _dataTable = value;
                setData();
                getColumns();
            }
        }

        /// <summary>
        /// Содержит массив номеров выделенных строк.
        /// </summary>
        /// <value>ArrayImpl</value>
        [ContextProperty("ВыделенныеСтроки", "SelectedRows")]
        public ArrayImpl SelectedRows
        {
            get
            {
                ArrayImpl list1 = new ArrayImpl();

                var dg = ((DataGridView)_item);
                var sr = dg.SelectedRows;
                foreach (DataGridViewRow el in sr)
                {
                    list1.Add(ValueFactory.Create(el.Index));
                }

                return list1;
            }
        }

        /// <summary>
        /// Номер текущей строки таблицы.
        /// </summary>
        [ContextProperty("ТекущаяСтрока", "CurrentRow")]
        public int CurrentRow
        {
            get { return _bindingSource.Position; }
            set { _bindingSource.Position = value; }
        }

        /// <summary>
        /// Представляет доступ к текущим данным (данным текущей строки).
        /// </summary>
        [ContextProperty("ТекущиеДанные", "CurrentData")]
        public ValueTableRow CurrentData
        {
            get
            {
                ValueTable tbl = _dataTable.Source;
                return tbl.Get(_bindingSource.Position);
            }
        }

        /// <summary>
        /// Обновляет данные в таблице.
        /// </summary>
        [ContextMethod("Обновить", "Refresh")]
        public void Refresh()
        {
            _dataTable.Refresh();
            //DataPath = _dataTable;
        }

        /// <summary>
        /// Колонки таблицы.
        /// </summary>
        /// <value>Массив <see cref="FormTableColumn"/></value>
        [ContextProperty("Колонки", "Columns")]
        public ArrayImpl Columns
        {
            get { return _columns; }
        }

        /// <summary>
        /// Текущее поле в таблице.
        /// </summary>
        [ContextProperty("ТекущийЭлемент", "CurrentItem")]
        public FormTableColumn CurrentItem
        {
            get {
                DataGridView dr = (DataGridView)_item;
                return (FormTableColumn)_columns.Get(dr.CurrentCell.ColumnIndex);
            }
        }


    }
}
