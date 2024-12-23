using MailSend.Enums;
using Microsoft.Data.SqlClient;

namespace MailSend.DB
{
    public class DataIntializer
    {
        private readonly string _connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MailDB; Trusted_Connection=True; TrustServerCertificate=True;";

        // Sample Data for EmailTemplate
        public void AddEmailTemplateSampleData()
        {
            string query = @"
            IF NOT EXISTS (SELECT 1 FROM EmailTemplates WHERE Title = @Title AND emailTypes = @EmailType)
            BEGIN
                INSERT INTO EmailTemplates (Id, emailTypes, Title, Body)
                VALUES (@Id, @EmailType, @Title, @Body);
            END";

            Guid emailTemplateId = Guid.NewGuid();
            EmailTypes emailType = EmailTypes.otp; 
            string title = "Verify OTP";
            string body = @"<!DOCTYPE html>
<html lang=""en"">
<head>
  <meta charset=""UTF-8"" />
  <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
  <title>Forgot Password OTP</title>
  <style>
    body {
      margin: 0;
      padding: 0;
      font-family: ""Helvetica Neue"", Helvetica, Arial, sans-serif;
      color: #333;
      background-color: #f9f9f9;
    }

    .container {
      margin: 20px auto;
      width: 100%;
      max-width: 600px;
      background-color: #ffffff;
      padding: 20px 15px;
      border-radius: 8px;
      box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
      box-sizing: border-box;
    }

    .header {
      text-align: center;
      padding-bottom: 20px;
      border-bottom: 1px solid #eee;
    }

    .header a {
      font-size: 1.5em;
      color: #00bc88;
      text-decoration: none;
      font-weight: bold;
    }

    .content {
      font-size: 1em;
      line-height: 1.6;
      color: #555;
    }

    .otp {
      display: flex;
      justify-content: center;
      align-items: center;
      margin: 15px 0;
      font-weight: bold;
      letter-spacing: 4px;
    }

    .otp h2 {
      margin: 0;
      border: 1px dashed #00bc88;
      padding: 10px 15px;
      font-size: 1.8em;
      color: #333;
    }

    .footer {
      text-align: center;
      color: #aaa;
      font-size: 0.85em;
      margin-top: 20px;
    }

    .footer a {
      text-decoration: none;
      color: #00bc88;
      font-weight: 500;
    }

    .email-info {
      color: #666;
      font-weight: 400;
      font-size: 0.85em;
      line-height: 1.4;
      margin-top: 10px;
    }

    .email-info a {
      text-decoration: none;
      color: #00bc88;
    }

    hr {
      border: none;
      border-top: 1px solid #ddd;
      margin: 20px 0;
    }

    @media screen and (max-width: 480px) {
      .container {
        padding: 15px;
      }

      .header a {
        font-size: 1.2em;
      }

      .otp h2 {
        font-size: 1.5em;
        padding: 8px 10px;
      }

      .content p {
        font-size: 0.95em;
      }

      .footer {
        font-size: 0.8em;
      }
    }
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">
      <a href=""#"">Forgot Password OTP</a>
    </div>

    <div class=""content"">
      <p><strong>Dear {Name},</strong></p>
      <p>
        We received a request to reset your password. To proceed, please use the following One-Time Password (OTP):
      </p>
      <div class=""otp"">
        <h2>{Otp}</h2>
      </div>
      <p>
        This OTP is valid for <strong>5 minutes</strong>. Please enter it on the password reset page to proceed.
      </p>
      <p>
        If you did not request a password reset, you can safely ignore this email. If you suspect unauthorized access, please contact our support team immediately.
      </p>
    </div>

    <hr />
    <div class=""footer"">
      <p>This is an automated email. Please do not reply.</p>
      <p>
        For further assistance, visit our support page or contact us at
        <a href=""mailto:info.way.mmakers@gmail.com"">info.way.mmakers@gmail.com</a>.
      </p>
      <div class=""email-info"">
        <a href=""#"">Way Makers</a> | No.127 Knnkesanthurai Road | Jaffna, Srilanka
      </div>
      <div class=""email-info"">
        &copy; 2024 Way Makers. All rights reserved.
      </div>
    </div>
  </div>
</body>
</html>
";

            ExecuteQuery(query, new Dictionary<string, object>
            {
                {"@Id", emailTemplateId},
                {"@emailType", emailType},
                {"@Title", title},
                {"@Body", body}
            });
        }

        private void ExecuteQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Sample data processed successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
