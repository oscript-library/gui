#Использовать asserts

Перем юТест;
Перем ВидыПоляФормы;
// Перем ПоложенияЗаголовка;
Перем ТекстПриИзменении;
Перем Форма;


Процедура Инициализация()
	ПодключитьВнешнююКомпоненту("oscript-component/bin/Release/oscript-simple-gui.dll");
КонецПроцедуры

Функция ПолучитьСписокТестов(Тестирование) Экспорт

	юТест = Тестирование;

	СписокТестов = Новый Массив;
	СписокТестов.Добавить("Тест_Должен_СоздатьТекстовоеПоле");
	СписокТестов.Добавить("Тест_Должен_ПровестиРаботуСоЗначением");
	СписокТестов.Добавить("Тест_Должен_УстановитьЗаголовок");
	СписокТестов.Добавить("Тест_Должен_УстановитьИмя");
	СписокТестов.Добавить("Тест_Должен_УстановитьВидимость");
	СписокТестов.Добавить("Тест_Должен_УстановитьДоступность");
	СписокТестов.Добавить("Тест_Должен_ВернутьРодителя");
	СписокТестов.Добавить("Тест_Должен_УстановитьТолькоПросмотр");
	СписокТестов.Добавить("Тест_Должен_ПроверитьПоложениеЗаголовка");
	
	СписокТестов.Добавить("Тест_Должен_УстановитьИПроверитьДействиеПриИзмении");
	СписокТестов.Добавить("Тест_Должен_ПолучитьДействие");
	
	Возврат СписокТестов;

КонецФункции

//# Работа с событиями
Процедура ПриОткрытииФормы() Экспорт
	Форма.Закрыть();
КонецПроцедуры

Процедура ПолучитьФорму()

	ПростойГУИ = Новый ПростойГУИ();
	Форма = ПростойГУИ.СоздатьФорму();
	Форма.УстановитьДействие(ЭтотОбъект, "ПриОткрытии", "ПриОткрытииФормы");
	ВидыПоляФормы = Форма.ВидГруппыФормы;

КонецПроцедуры

Процедура Тест_Должен_СоздатьТекстовоеПоле() Экспорт

	ПолучитьФорму();
	Форма.Показать();	

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;

	юТест.ПроверитьНеРавенство(Форма.Элементы.Найти("Поле1"), Неопределено);

КонецПроцедуры

Процедура Тест_Должен_ПровестиРаботуСоЗначением() Экспорт

	ПолучитьФорму();
	Форма.Показать();	

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.Значение = "ПроверкаЗначения";

	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").Значение, "ПроверкаЗначения");

КонецПроцедуры

Процедура Тест_Должен_УстановитьЗаголовок() Экспорт

	НовыйЗаголовок = "Новый заголовок";

	ПолучитьФорму();
	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.Заголовок = НовыйЗаголовок;
	Форма.Показать();

	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").Заголовок, НовыйЗаголовок);

КонецПроцедуры

Процедура Тест_Должен_УстановитьИмя() Экспорт

	НовоеИмя = "НовоеИмя";

	ПолучитьФорму();
	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.Имя = НовоеИмя;
	Форма.Показать();

	юТест.ПроверитьРавенство(Форма.Элементы.Найти(НовоеИмя).Имя, НовоеИмя);
КонецПроцедуры

Процедура Тест_Должен_УстановитьВидимость() Экспорт

	ПолучитьФорму();
	Форма.Показать();

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.Видимость = Истина;
	юТест.ПроверитьРавенство(Поле1.Visible, Истина);

	Поле1.Видимость = Ложь;
	юТест.ПроверитьРавенство(Поле1.Visible, Ложь);
	
КонецПроцедуры

Процедура Тест_Должен_УстановитьДоступность() Экспорт

	ПолучитьФорму();
	Форма.Показать();

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.Доступность = Истина;

	юТест.ПроверитьРавенство(Поле1.Enabled, Истина);

	Поле1.Доступность = Ложь;
	юТест.ПроверитьРавенство(Поле1.Enabled, Ложь);
	
КонецПроцедуры

Процедура Тест_Должен_ВернутьРодителя() Экспорт

	ПолучитьФорму();
	Форма.Показать();

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;

	юТест.ПроверитьРавенство(Строка(Форма.Элементы.Найти("Поле1").Родитель), "УправляемаяФорма");

	Группа = Форма.Элементы.Добавить("Группа", "ГруппаФормы", Неопределено);

	Поле2 = Форма.Элементы.Добавить("Поле2", "ПолеФормы", Группа);
	юТест.ПроверитьРавенство(Группа.Элементы.Найти("Поле2").Родитель, Группа);

	//# Не должно быть у формы
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле2"), Неопределено);
	
КонецПроцедуры

Процедура Тест_Должен_УстановитьТолькоПросмотр() Экспорт

	ПолучитьФорму();
	Форма.Показать();

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.ТолькоПросмотр = Истина;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ReadOnly, Истина);

	Поле1.ТолькоПросмотр = Ложь;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ReadOnly, Ложь);
	
КонецПроцедуры

Процедура Тест_Должен_ПроверитьПоложениеЗаголовка() Экспорт
	
	ПолучитьФорму();
	Форма.Показать();

	ПоложенияЗаголовка = Форма.ПоложениеЗаголовка;

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Авто;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Авто);

	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Верх;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Верх);

	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Лево;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Лево);

	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Нет;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Нет);

	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Низ;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Низ);

	Поле1.ПоложениеЗаголовка = ПоложенияЗаголовка.Право;
	юТест.ПроверитьРавенство(Форма.Элементы.Найти("Поле1").ПоложениеЗаголовка, ПоложенияЗаголовка.Право);


КонецПроцедуры


//# Работа с событиями
Процедура ПриИзменииЗначения() Экспорт
	ТекстПриИзменении = "Новое значение: ";
КонецПроцедуры

Процедура Тест_Должен_УстановитьИПроверитьДействиеПриИзмении() Экспорт
	ПолучитьФорму();
	
	ТекстПриИзменении = "Исходный текст: ";

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.УстановитьДействие(ЭтотОбъект, "ПриИзменении", "ПриИзменииЗначения");
	Поле1.Значение = "Произвольный текст";

	юТест.ПроверитьРавенство(ТекстПриИзменении + Форма.Элементы.Найти("Поле1").Значение, "Новое значение: Произвольный текст");

	Форма.Показать();
	//Форма.Показать();

КонецПроцедуры

Процедура Тест_Должен_ПолучитьДействие() Экспорт
	ПолучитьФорму();

	Поле1 = Форма.Элементы.Добавить("Поле1", "ПолеФормы", Неопределено);
	Поле1.Вид = Форма.ВидПоляФормы.ПолеВвода;
	Поле1.УстановитьДействие(ЭтотОбъект, "ПриИзменении", "ПриИзменииЗначения");

	юТест.ПроверитьНеРавенство(Форма.Элементы.Найти("Поле1").ПолучитьДействие("ПриИзменении"), "");

	Форма.Показать();

КонецПроцедуры

//////////////////////////////////////////////////////////////////////////////////////
// Инициализация

Инициализация();
