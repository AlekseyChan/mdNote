using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace mdNote.Pages
{
    public class HelpPage : ContentPage
    {
        public const string HelpString = @"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {
            font-size: 12pt;
            font-family: sans-serif;
            background: #ddd;
            padding: 5px;
        }

        pre {
            overflow: auto;
            word-wrap: normal;
            white-space: pre-wrap;
            background: #fffcf0;
            padding: 10px;
        }

            pre h1 {
                margin: 0;
            }

            pre h2 {
                margin: 0;
            }

            pre h3 {
                margin: 0;
            }

            pre h4 {
                margin: 0;
            }

            pre h5 {
                margin: 0;
            }

            pre h6 {
                margin: 0;
            }
    </style>
</head>
<body>
    <h1>Markdown guide</h1>
    <h2>Headers</h2>
    <pre><h1>H1</h1>
    <h2>H2</h2>
    <h3>H3</h3>
    <h4>H4</h4>
    <h5>H5</h5>
    <h6>H6</h6></pre>
    <em>Alternatively, for H1 and H2, an underline-ish style:</em>
    <pre><h1>Alt-H1<br />======</h1>
    <h2>Alt-H2<br />------</h2></pre>
    <h2>Emphasis</h2>
<pre>**<strong>bold</strong>**
*<em>italics</em>*
~~<strike>strikethrough</strike>~~</pre>
    <h2>Lists</h2><pre>
* Generic list item
+ Generic list item
- Generic list item

1. Numbered list item
2. Numbered list item
3. Numbered list item</pre><em>or</em><pre>
1) Numbered list item
2) Numbered list item
3) Numbered list item</pre><em>Use indents to add subitems</em><pre>
* item
    * sub item</pre>
    <h2>Links</h2><pre>[Text to display](http://www.example.com)</pre>
    <h2>Quotes</h2>
<pre>&gt; This is a quote.
&gt; It can span multiple lines!</pre>
    <h2>Images</h2>
<pre>![alt text](http://www.example.com/image.jpg)</pre>

    <h2>Tables</h2>
<pre>
Column 1 | Column 2 | Column 3
-------- |:--------:| --------:
some data | center aligned data | right aligned data</pre>
<em>will produce</em>
<pre><table width='100%' border='1'>
    <thead>
    <th>Column 1</th>
    <th align = 'center' > Column 2</th>
    <th align = 'right' > Column 3</th>
    </thead>
    <tbody>
    <td>some data</td>
    <td align = 'center' > center aligned data</td>
    <td align = 'right' > right aligned data</td>
    </tbody>
</table></pre>

    <h2>Displaying code</h2>
<pre>`var example = 'hello!';`</pre>

<em>Or spanning multiple lines...</em>

<pre>```
var example = 'hello!';
alert(example);
```</pre>

    <h2>Horizontal rule</h2>
<em>use line of three asterisks, hyphens or underscores for horizontal rule:</em>
    <pre>***
---
___
</pre>

    <h2>Line breaks</h2>
    <em>Use two line breaks to start new paragraph.</em>
</body>
</html>";
        WebView webView;
        public HelpPage()
        {
            webView = new WebView()
            {

                Source = new HtmlWebViewSource
                {
                    BaseUrl = Services.DeviceServices.BaseUrl,
                    Html = HelpString
                }
            };
            Content = webView;
        }
    }
}