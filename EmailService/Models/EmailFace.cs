using System;
using System.Collections.Generic;
using System.Text;

namespace EmailService.Models
{
    public static class EmailFace
    {
        public const string Up = @"<!DOCTYPE html>
                                    <html>
                                        <head>
                                            <meta charset='utf-8'>
                                            <title>Почтовий Сервис</title>
                                        </head>
                                        <body>
                                            <div>";

        public const string Down = @"</div>
                                        </body>
                                    </html>";
    }
}
