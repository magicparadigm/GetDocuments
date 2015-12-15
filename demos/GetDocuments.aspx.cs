using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.IO;

using System.Collections;
using Newtonsoft.Json;

using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Net.Http;

public partial class demos_GetDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            button.InnerText = "Get Documents";
        }

        PrefillClick.ServerClick += new EventHandler(prefill_Click);
        button.ServerClick += new EventHandler(button_Click);
    }

    protected void prefill_Click(object sender, EventArgs e)
    {
        accountEmail.Value      = "magicparadigm@outlook.com";
        integratorkey.Value     = "FINA-311bdb00-ad59-4750-924d-ee720aeaa43e";
        envelopeId.Value        = "1D9254C5-D94C-41C1-92BA-E76B6471B131";
        accountId.Value         = "d4af19ac-22ac-4018-a2db-062d361fd569";
    }


    public class EnvelopeDocument
    {
        public string documentId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
        public string order { get; set; }
        public string pages { get; set; }
    }

    public class GetEnvelopeAssetsResponse
    {
        public string envelopeId { get; set; }
        public List<EnvelopeDocument> envelopeDocuments { get; set; }
    }

    protected void button_Click(object sender, EventArgs e)
    {
        String securityHeader = "<DocuSignCredentials>" + "<Username>" + accountEmail.Value + "</Username>" + "<Password>" + password.Value + "</Password>" + "<IntegratorKey>" + integratorkey.Value + "</IntegratorKey>" + "</DocuSignCredentials>";

        // create the request used to create an envelope
        HttpWebRequest request = HttpWebRequest.Create(ConfigurationManager.AppSettings["DocuSignServer"] + "/restapi/v2/accounts/" + accountId.Value + "/envelopes/" + envelopeId.Value + "/documents") as HttpWebRequest;
        request.Method = "GET";

        request.Headers["X-DocuSign-Authentication"] = securityHeader;
        request.Accept = "application/json";
        request.ContentType = "application/json";

        try
        {
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            Console.WriteLine("\nStatus Code: " + response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                byte[] responseBytes = new byte[response.ContentLength];
                using (var reader = new System.IO.BinaryReader(response.GetResponseStream()))
                {
                    reader.Read(responseBytes, 0, responseBytes.Length);
                }
                string responseText = Encoding.UTF8.GetString(responseBytes);
                GetEnvelopeAssetsResponse getEnvelopeAssetsResponse = new GetEnvelopeAssetsResponse();

                getEnvelopeAssetsResponse = JsonConvert.DeserializeObject<GetEnvelopeAssetsResponse>(responseText);

                // Create a documents directory if one does not exist
                if (!Directory.Exists(Server.MapPath("./documents/")))
                {
                    Directory.CreateDirectory(Server.MapPath("./documents/"));
                }

                // Retrieve each document within the document and store in documents directory
                String listHTML = "";
                foreach (EnvelopeDocument envelopeDocument in getEnvelopeAssetsResponse.envelopeDocuments)
                {
                    HttpWebRequest request2 = HttpWebRequest.Create(ConfigurationManager.AppSettings["DocuSignServer"] + "/restapi/v2/accounts/" + accountId.Value + envelopeDocument.uri) as HttpWebRequest;
                    request2.Method = "GET";
                    request2.Accept = "application/pdf";
                    request2.Headers["X-DocuSign-Authentication"] = securityHeader;

                    HttpWebResponse response2 = request2.GetResponse() as HttpWebResponse;
                    Console.WriteLine("\nStatus Code: " + response2.StatusCode);

                    if (response2.StatusCode == HttpStatusCode.OK)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (FileStream outfile = new FileStream(Server.MapPath("./documents/") + envelopeDocument.name, FileMode.Create))
                            {
                                response2.GetResponseStream().CopyTo(ms);
                                if (ms.Length > int.MaxValue)
                                {
                                    throw new NotSupportedException("Cannot write a file larger than 2GB.");
                                }
                                outfile.Write(ms.GetBuffer(), 0, (int)ms.Length);
                            }
                        }
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        url = url.Substring(0, url.LastIndexOf(@"/") + 1);
                        if (envelopeDocument.name.Equals("Summary"))
                            url = url + "/documents/Summary.pdf";
                        else
                            url = url + "/documents/" + envelopeDocument.name;    

                        listHTML += "<li><a onclick=\"window.open('" + url + "'); return false;\" href=\"" + url + "\">" + envelopeDocument.name + "</a></li>";
                    }
                    
                }
                documentsList.InnerHtml = listHTML;    
            }
        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                using (var reader = new System.IO.StreamReader(ex.Response.GetResponseStream(), UTF8Encoding.UTF8))
                {
                    string errorMess = reader.ReadToEnd();
                    log4net.ILog logger = log4net.LogManager.GetLogger(typeof(demos_GetDocuments));
                    logger.Info("\n----------------------------------------\n");
                    logger.Error("DocuSign Error: " + errorMess);
                    logger.Error(ex.StackTrace);
                    Response.Write(ex.Message);
                }
            }
            else
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(typeof(demos_GetDocuments));
                logger.Info("\n----------------------------------------\n");
                logger.Error("WebRequest Error: " + ex.Message);
                logger.Error(ex.StackTrace);
                Response.Write(ex.Message);
            }
        }
    }
}