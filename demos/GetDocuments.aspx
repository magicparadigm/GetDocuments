<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetDocuments.aspx.cs" Inherits="demos_GetDocuments" %>

<!DOCTYPE html>
<html class="no-js" lang="">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Get Documents</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="apple-touch-icon">

    <!-- Styles and Fonts -->
    <link rel="stylesheet" href="../style/screen.css">
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700,300' rel='stylesheet' type='text/css'>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>

    <script>
        function updateWindowSize() {
            var width = window.innerWidth ||
                        document.documentElement.clientWidth ||
                        document.body.clientWidth;
            var height = window.innerHeight ||
                            document.documentElement.clientHeight ||
                            document.body.clientHeight;
            docusignFrame.height = height - 130;
            docusignFrame.width = width;

        }

        window.onload = updateWindowSize;
        window.onresize = updateWindowSize;
    </script>
</head>
<body class="finance">

    <div class="demo">For demonstration purposes only.</div>

    <header>
        <div class="container-fixed">

            <nav class="navbar">
                <div class="navbar-mini">
                    <ul>
                        <li><a href="https://github.com/magicparadigm/GetDocuments">Source Code</a></li>
                        <li><a href="https://www.docusign.com/developer-center">DocuSign DevCenter</a></li>
                    </ul>
                </div>
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#collaps0r">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="default.aspx">Get Documents Example</a>
                </div>
            </nav>

        </div>
    </header>

    <div id="mainForm" runat="server" class="container-fixed formz-vertical">
        <br />
        <form class="form-inline" runat="server" id="form">
            <div class="row">
                <div class="col-xs-12">
                    <h1><a id="PrefillClick" runat="server" href="#">Get documents from an Envelope</a></h1>

                </div>
            </div>
            <div class="row" id="authenticationSection" runat="server">
                <div class="col-xs-12">
                    <h2>Authentication</h2>
                    <div class="form-group">
                        <label for="accountemail">Account Email</label>
                        <input type="email" runat="server" class="form-control" id="accountEmail" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="password">Password</label>
                        <input type="password" runat="server" class="form-control" id="password" placeholder="" style="width: 556px">
                    </div>
                    <div class="form-group">
                        <label for="integratorkey">integratorkey</label>
                        <input type="text" runat="server" class="form-control" id="integratorkey" placeholder="" style="width: 556px">
                    </div>
                    <br>

                    <hr />
                </div>
            </div>
            <div class="row" id="Parameters" runat="server">
                <div class="col-xs-12">
                    <h2>Parameters</h2>
                    <div class="form-group">
                        <label for="envelopeid">Envelope ID</label>
                        <input type="text" runat="server" class="form-control" id="envelopeId" placeholder="" style="width: 556px">
                    </div>
                    <div class="form-group">
                        <label for="accountid">Account ID</label>
                        <input type="text" runat="server" class="form-control" id="accountId" placeholder="" style="width: 556px">
                    </div>
                    <br>
                    <hr />
                </div>
            </div>
            <div class="row" id="Documents" runat="server">
                <div class="col-xs-12">
                    <h2>List Documents</h2>
                    <div class="form-group">
                        <ul id="documentsList" style="font-weight: 700; font-size: 18px;" runat="server" />
                    </div>
                    <br>
                    <hr />

                </div>
            </div>
            <button type="button" visible="true" id="button" runat="server" class="btn" style="color: #fff; padding: 10px 80px; font-size: 14px; margin: 40px auto; display: block;" />
        </form>
    </div>


    <!-- Google Analytics -->
    <script>
        (function (b, o, i, l, e, r) {
            b.GoogleAnalyticsObject = l; b[l] || (b[l] =
            function () { (b[l].q = b[l].q || []).push(arguments) }); b[l].l = +new Date;
            e = o.createElement(i); r = o.getElementsByTagName(i)[0];
            e.src = '//www.google-analytics.com/analytics.js';
            r.parentNode.insertBefore(e, r)
        }(window, document, 'script', 'ga'));
        ga('create', 'UA-XXXXX-X', 'auto'); ga('send', 'pageview');
    </script>

    <!-- Scripts -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="../js/main.js"></script>

    <script type='text/javascript' id="__bs_script__">
        document.write("<script async src='//localhost:3000/browser-sync/browser-sync-client.1.9.0.js'><\/script>".replace(/HOST/g, location.hostname).replace(/PORT/g, location.port));
    </script>
</body>
</html>
