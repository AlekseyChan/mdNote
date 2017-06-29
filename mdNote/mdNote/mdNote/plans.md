# Планирование разработки

## Рефакторинг

1. выяснить, что не так с Device.OnPlatform. Метод устарел (например в классе IconFont)
2. после 


## Варианты дизайна

1. С отдельным предпросмотром
2. Предпросмотр как в GhostWriter предпочтительней

## ContentView

1. FolderBrowser - навигатор по каталогам и файлам
2. Editor - Тут все есть
3. RecentFiles 
4. About
5. Settings
	1. Точка входа: последние, места, новая заметка
	2. шрифты
	3. цвета
	4. сервис парсинга ссылок
	5. показывать только поддерживаемые типы файлов
	6. предпросмотр по умолчанию,
	7. linewrapping

## Точки входа для Android

1. Главная страница (в зависимости от настройки)
2. просто редактор для текста и ссылок
3. редактор с предварительной загрузкой страницы для ссылок
4. append to

## Настройки

1. AutoSplit - если ориентация экрана горизонтальная, то делим экран на две части - редактор и предпросмотр
2. css - этот файл по идее должен генерироваться в зависимости от темы приложения. Можно сразу много забить
3. AutoPreview - если экран не разделен, то показывать вначале предпросмотр
4. PreviewOnLeft - если экран разделен, то показывать предпросмотр слева

## Проблематика

1. Надо придумать как синхронизировать скролинг в двух частях экрана надо изучить что умеет WebView
2. Надо придумать как показать номера строк
3. Надо придумать как редактировать RichEdit

## Инструкции и компоненты

https://components.xamarin.com/view/shareplugin - Плагин для кнопки "Поделиться"
https://forums.xamarin.com/discussion/21237/sharing-to-an-app

## Вдохновение

https://github.com/Code52/MarkPadRT/tree/master/src/MarkPad приложение под Win8

Возможно стоит использовать RazorBlade https://developer.xamarin.com/guides/cross-platform/advanced/razor_html_templates/ и просто воткнуть какой-нибудь редактор типа StackEdit.io или еще чего в этом духе, написанное на JavaScript

https://simplemde.com/

http://www.developersfeed.com/awesome-javascript-wysiwyg-markdown-editors/

http://ourcodeworld.com/articles/read/359/top-7-best-markdown-editors-javascript-and-jquery-plugins


## Render android

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace Mobile.Droid
{    
    public class WebViewRender : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as WebViewer;
            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                    var reset = new ManualResetEvent(false);
                    var response = string.Empty;
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Control?.EvaluateJavascript(js, new JavascriptCallback((r) => { response = r; reset.Set(); }));
                    });
                    await Task.Run(() => { reset.WaitOne(); });
                    return response;
                };
        }
    }
   
    internal class JavascriptCallback : Java.Lang.Object, IValueCallback
    {
        public JavascriptCallback(Action<string> callback)
        {
            _callback = callback;
        }

        private Action<string> _callback;
        public void OnReceiveValue(Java.Lang.Object value)
        {
            _callback?.Invoke(Convert.ToString(value));
        }
    }
}

## Получение текста

Чтобы лучше понять различные режимы работы фильтров Intent, рассмотрите следующий фрагмент из файла манифеста приложения для работы с социальными сетями.
<activity android:name="MainActivity">
    <!-- This activity is the main entry, should appear in app launcher -->
    <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
    </intent-filter>
</activity>

<activity android:name="ShareActivity">
    <!-- This activity handles "SEND" actions with text data -->
    <intent-filter>
        <action android:name="android.intent.action.SEND"/>
        <category android:name="android.intent.category.DEFAULT"/>
        <data android:mimeType="text/plain"/>
    </intent-filter>
    <!-- This activity also handles "SEND" and "SEND_MULTIPLE" with media data -->
    <intent-filter>
        <action android:name="android.intent.action.SEND"/>
        <action android:name="android.intent.action.SEND_MULTIPLE"/>
        <category android:name="android.intent.category.DEFAULT"/>
        <data android:mimeType="application/vnd.google.panorama360+jpg"/>
        <data android:mimeType="image/*"/>
        <data android:mimeType="video/*"/>
    </intent-filter>
</activity>
Первая операция (MainActivity) является основной точкой входа приложения — это операция, которая открывается, когда пользователь запускает приложение нажатием на его значок:
Действие ACTION_MAIN указывает на то, что это основная точка входа, и не ожидает никаких данных объекта Intent.
Категория CATEGORY_LAUNCHER указывает, что значок этой операции следует поместить в средство запуска приложений системы. Если элемент &lt;activity&gt; не содержит указаний на конкретный значок с помощью icon, то система воспользуется значком из элемента &lt;application&gt; .
Чтобы операция отображалась в средстве запуска приложений системы, два этих элемента необходимо связать вместе.
Вторая операция (ShareActivity) предназначена для упрощения обмена текстовым и мультимедийным контентом. Несмотря на то, что пользователи могут входить в эту операцию, выбрав ее из MainActivity, они также могут входить в ShareActivity напрямую из другого приложения, которое выдает неявный объект Intent, соответствующий одному из двух фильтров Intent.
Примечание. Тип MIME (application/vnd.google.panorama360+jpg) является особым типом данных, указывающим на панорамные фотографии, с которыми можно работать с помощью API-интерфейсов Google panorama.

## Как делиться

Call CrossShare.Current from any project or PCL to gain access to APIs.

Share
/// <summary>
/// Simply share text on compatible services
/// </summary>
/// <param name="text">Text to share</param>
/// <param name="title">Title of popup on share (not included in message)</param>
/// <returns>awaitable Task</returns>
Task Share(string text, string title = null);


Open Browser
/// <summary>
/// Open a browser to a specific url
/// </summary>
/// <param name="url">Url to open</param>
/// <returns>awaitable Task</returns>
Task OpenBrowser(string url);

https://code.msdn.microsoft.com/windowsapps/Sharing-Content-Target-App-e2689782/sourcecode?fileId=44814&pathId=298223712

https://github.com/Microsoft/Windows-universal-samples/tree/master/Samples/ShareTarget


sealed partial class App : Application

    {

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)

        {

            var rootFrame = CreateRootFrame();

            rootFrame.Navigate(typeof(ShareTarget), args.ShareOperation);

            Window.Current.Activate();

        }

    }