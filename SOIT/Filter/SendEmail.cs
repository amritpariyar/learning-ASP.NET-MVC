using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace Banijya.Services
{
    public class SendEmail
    {
        protected static string SMTP = "mail.doc.gov.np";
        protected static string FromMail = "no-reply@doc.gov.np";
        protected static string fromPass = "Default12345";
        //protected static string SMTP = "mail.gatewaytechnologies.com.np";
        //protected static string FromMail = "info@gatewaytechnologies.com.np";
        //protected static string fromPass = "G100%sucess";
        protected static int PORT = 25;

        public static string LiveURL { get; set; }
        public static string UserName { get; set; }
        public static string PassWord { get; set; }
        public static string PassKey { get; set; }
        public static string userID { get; set; }
        public static string Subject { get; set; }
        public static string toMail { get; set; }

        internal static string messageTitle = @"<div class='dartaheadtitle' style='height: unset; float: left;position: relative;display:       block;width: 100%; text-align: center;font-size: 20px;font-weight: bold;color: #e25805;'>
                                  <span class='row1' style='display:block;width:100%'>नेपाल सरकार</span>
                                  <span class='row2' style='display:block;width:100%'>उद्योग, वाणिज्य तथा आपूर्ति मन्त्रालय</span>
								  <span class='row3' style='display:block;width:100%'>वाणिज्य, आपूर्ति तथा उपभोक्ता संरक्षण विभाग</span>
                                <span class='row4' style='line-height:1;margin:8px 0px 15px 0px;display:block;width:100%;'>(व्यवसायिक फर्म व्यवस्थापन सूचना प्रणाली)</span>
                            
                        </div>
                    </br></br></br>
                वाणिज्य विभागको अन्लाइन फर्म दर्ता प्रणाली प्रयोग गर्नु भएकोमा धन्यवाद ! </br>
                ";
        internal static string messageFooter = @"<footer>
                    <span style='text-align:center'>हामि निरन्तर सुधारमा छौ ।</span></br>
                    <div class='bottom-sologan' style='padding-left:35%; font-weight:700; '>
                    <div><u><strong>सम्पर्क</strong></u></div>
                    <div>बबरमहल, काठ्माण्डौ, नेपाल</div>
                    <div>फोन नं.: +९७७-१-४२४३९३९, ४२४७९१२, ४२३९१२३</div>
                    <div>फ्याक्स : +९७७-१-४२४९६०३</div>
                    <div>ईमेल : info@doc.gov.np</div>
                    <div>वेवसाईट : www.doc.gov.np</div>
                    <span>
                        &nbsp; &nbsp; &nbsp; 'सहज आपुर्तिको मूल आधार <br>
                    जागरूक उपभोक्ता र प्रतिस्पर्धी बजार'
                </span>
            </div></hr>
    <span style='text-align:center'><small>(यो Automated Email Service हो । यसमा Reply नगर्नुहोला)</small></span></footer>";


        public static string ForgotPasswordFormat
        {
            get
            {
                return @"Dear {user},<br><br>
                            Your password has been changed as follows <br>
                            User Name: <b>" + UserName + @"</b>
                            <br>
                            Pass Word: <b>{password}</b>
                            <br>

                            <a href='" + LiveURL + @"/Login'>Login Here</a>
                            <br> OR <br>

                            " + LiveURL + @"/Login

                            <footer>
                            Do not reply
                            </footer>

                            ";
            }
        }

        public static string ActivateSubscriptionFormat
        {
            get
            {
                return @"Dear {user},<br><br>
                            Your Request for subscription is waiting for approval<br>
                            

                            <a href='" + LiveURL + @"/Signup/Confirm/{sub_id}'>Activat here</a>
                            <br> OR <br>

                            " + LiveURL + @"/Signup/Confirm/{sub_id}

                            <footer>
                            Do not reply
                            </footer>

                            ";
            }
        }

        public static string SubscriptionConfirmFormat
        {
            get
            {
                return @"Dear {user},<br><br>
                            Your Request for subscription is approved. Following are the login credential: <br>
                            
                            User name: {username} <br>
                            Pass word: {password} <br>


                            <a href='" + LiveURL + @"/Login'> Login Here</a>
                            <br> OR <br>

                            " + LiveURL + @"/Login

                            <footer>
                            Do not reply
                            </footer>

                            ";
            }
        }

        public static bool SendEmailPasswordActivation()
        {
            try
            {
                MailMessage mail = new MailMessage(FromMail, toMail);
                int port = Convert.ToInt32(PORT);
                string url = "http://doc.gov.np/OnlineUser/Login";

                string login_id = userID;

                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = "Dear " + UserName + "<br />";
                mail.Body += @"Your login details ;<br/>
                           Username : " + UserName + @"<br />
                           Password : " + PassWord + "<br />";
                mail.Body += "<hr/> To modify your password,<a href=" + url + @">Login Here</a><br /> and go to user detail page .<hr/>";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(SMTP, port);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromMail, fromPass);

                return SendMe(smtp, mail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendEmailFirmAccepatance()
        {
            try
            {
                string url = "http://doc.gov.np/OnlineUser/Login";
                MailMessage mail = new MailMessage(FromMail, toMail);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = messageTitle + "<span> महोदय " + UserName + ",</span>";
                mail.Body += "<span>तपाईंको  फर्म रजिष्ट्रेशनको लागी स्विकृत भएको छ। </span>";
                mail.Body += @"<span>तपाईंको अनलाइन फर्म दर्ताको Token No:<strong> " + PassKey + @"</strong> </span>
                          <span>कृपया यो Token No लिएर वाणिज्य विभागमा फर्म दर्ताका लागी ७ (सात) दिन भित्र कार्यालयमा सम्पर्क राख्नुहोला।</span> 
                          <span>धन्यवाद ! </span>" + messageFooter;

                int port = Convert.ToInt32(PORT);
                string login_id = userID;
                SmtpClient smtp = new SmtpClient(SMTP, port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromMail, fromPass);
                return SendMe(smtp, mail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendEmailFirmRejection(string remarks)
        {
            try
            {
                int port = Convert.ToInt32(PORT);
                string url = "http://doc.gov.np/OnlineUser/Login";
                string login_id = userID;

                MailMessage mail = new MailMessage(FromMail, toMail);
                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = messageTitle +
                    "<span>महोदय " + UserName + ",</span>";
                mail.Body += @"<span>तपाईंको फर्म रजिष्ट्रेशनको लागी अस्विकृत भएको छ...  ;</span>
                            <span>कैफियत : " + remarks + "</span>" +
                            "<span>यस सम्बन्धी जिज्ञासाको लागी कार्यालयमा सम्पर्क गर्नुहोला । धन्यवाद</span> " +
                            messageFooter;

                SmtpClient smtp = new SmtpClient(SMTP, port);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromMail, fromPass);
                return SendMe(smtp, mail);
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private static bool SendMe(SmtpClient smtp, MailMessage mail)
        {
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool NewUserCreate()
        {
            try
            {
                MailMessage mail = new MailMessage(FromMail, toMail);

                int port = Convert.ToInt32(PORT);
                string url = "http://doc.gov.np/Account/Login";

                string login_id = userID;

                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = "Dear " + UserName + "<br />";
                mail.Body += @"Your account has been created for the FMIS system...  ;<br/>
                                Please use Username and password to login as given below:<br/>
                                USERNAME: " + UserName + @"<br/>PASSWORD: " + PassWord + @"<br/>
                           for futher query, please contact Office.<br/> 
                           To login the system go to the link : <a href='" + url + @"' target='_blank'>" + url + @"</a><br/>
                           Please change your password after successfull login. ";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(SMTP, port);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromMail, fromPass);

                return SendMe(smtp, mail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool EditUserMail()
        {
            try
            {
                MailMessage mail = new MailMessage(FromMail, toMail);

                int port = Convert.ToInt32(PORT);
                string url = "http://doc.gov.np/Account/Login";

                string login_id = userID;

                mail.Subject = Subject;
                mail.IsBodyHtml = true;
                mail.Body = "Dear " + UserName + "<br />";
                mail.Body += @"Your account has been edited for the FMIS system...  ;<br/>
                                Please use Username and password to login as given below:<br/>
                                USERNAME: " + UserName + @"<br/>PASSWORD: " + PassWord + @"<br/>
                           for futher query, please contact Office.<br/> 
                           To login the system go to the link : <a href='" + url + @"' target='_blank'>" + url + @"</a><br/>
                           Please change your password after successfull login. ";

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(SMTP, port);

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(FromMail, fromPass);

                return SendMe(smtp, mail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendTestEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.doc.gov.np");

                mail.From = new MailAddress("no-reply@doc.gov.com");
                mail.To.Add("amrit.pariyar1@gmail.com");
                mail.Subject = "Test Mail";
                mail.IsBodyHtml = true;
                mail.Body = messageTitle + "<span> महोदय ,</span>";
                mail.Body += "<span>तपाईंको  फर्म रजिष्ट्रेशनको लागी स्विकृत भएको छ। </span>";
                mail.Body += @"<span>तपाईंको अनलाइन फर्म दर्ताको Token No:<strong> 123456789012</strong> </span>
                          <span>कृपया यो Token No लिएर वाणिज्य विभागमा फर्म दर्ताका लागी ७ (सात) दिन भित्र कार्यालयमा सम्पर्क राख्नुहोला।</span> 
                          <span>धन्यवाद ! </span>" + messageFooter;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("no-reply@doc.gov.np", "Default12345");
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}