# CF-Stream-Uploader

[![.NET Core](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml)

## Config

The JSON config file has the following format.

```
{
  "CfToken": "exampleToken",
  "VideoId": "exampleVideoId",
  "KeyId": "exampleKeyId",
  "PrivateKey": "examplePrivateKey",
  "ExpiresIn": 0,
  "IsDarkmode": false
}
```

## HTML-Output

### Default HTML-Output

If you dont have a own HTML-Layout the App will generare a default HTML-Layout.
This is the default HTML-Layout:

```
<div style="position: relative; padding-top: 56.25%;">
        <iframe src="https://iframe.videodelivery.net/{0}?preload=true"
                style="border: none;
                position: absolute;
                top: 0;
                height: 100%;
                width: 100%;"
                allow="accelerometer;
                gyroscope;
                autoplay;
                encrypted-media;
                picture-in-picture;"
                allowfullscreen="true">
        </iframe>
</div>
```

### Personalized HTML-Output

There again you can copie your own Html-Layout file named "HtmlLayout.txt" in:
```
C:\Users\LOCAL_USER\AppData\Roaming\CfStreamUploader\ 
```
(make sure to exchange "LOCAL_USER" with you local windows user name ;) )

You need one (and only one) variale in your "HtmlLayout.txt" file. 
Here is an example:

```
src="https://iframe.videodelivery.net/{0}?preload=true"
```
{0} will replace with the given video token.