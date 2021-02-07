# BonusSystem
Написать клиент-серверное приложение «Бонусная система».
Язык C#, платформа .Net Core, WPF(желательно)/Windows Forms
Функционал:
Поиск бонусной карты клиента по номеру телефона.
Поиск бонусной карты по её номеру.
Отражение баланса бонусной карты.
Списание средств с бонусной карты. Учесть срок действия.
Создание бонусной карты. Номер бонусной карты присваивается автоматически. Он цифровой и уникальный*. Длина не более 6 цифр.
Зачисление средств на карту.  
Приложение должно быть отказоустойчивым. При вводе некорректных данных приложение не должно «падать».
Дизайн – на усмотрение разработчика.
* разработать эффективный механизм генерации и проверки на уникальность для случаев, когда диапазон доступных номеров практически закончился.
Объекты:
1.       Бонусная карта.
1.1.  Номер.
1.2.  Дата окончания действия.
1.3.  Баланс.
2.       Клиент.
2.1.  ФИО.
2.2.  Номер телефона.
 
В качестве серверной части желательно использование Web-сервисов написанных с использованием ASP.Net. Core. Это будет преимуществом при рассмотрении проекта.
В случае незнания технологии - любая реализация клиент-серверного взаимодействия.
Хранение начальных данных организовать любым способом.





appsettings.json : 

{

  "ConnectionStrings": {

    "EnterpriseDbContext": ""
  },

  "MailConfiguration": {
    "SmtpClientHost": "smtp.gmail.com",
    "SmtpClientPort": "465",
    "SmtpClientEnableSsl": "true",
    "SmtpClientCredentialUser": "sergeshelipov@gmail.com",
    "SmtpClientCredentialPassword": "365365365365",
    "SmtpClientAdminEmail": "sergeshelipov@gmail.com",
    "SmtpClientRedirectEmail": "sergeshelipov@gmail.com"
  },

  "ElasticSearchConfiguration": {
    "ElasticSearchUrl": "http://github",
    "ElasticSearchLogin": "",
    "ElasticSearchPassword": "",
    "ElasticSearchDABEnvirment": "sergeshelipov@gmail.com",
    "ElasticSearchCheck": "false"
  }
}
