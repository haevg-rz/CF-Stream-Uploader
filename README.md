# CF-Stream-Uploader

[![.NET Core](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml/badge.svg?branch=main)](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml)


The CF-Stream-Uploader is an UI to upload your videos in your [cloudflare](https://www.cloudflare.com/de-de/) cloud.


### Features

- You can easily upload your videos in your own [cloudflare](https://www.cloudflare.com/de-de/) cloud
- Additional you can set restrictions for all your videos
- The CF-Stream-Uploader will genereate a HTML-Code with your embedded videolink
- You can personalize the HTML-Code
- The CF-Stream-Uploader displays your video-storage-capacity for an easy overview
- The CF-Stream-Uploader saves your HTML-Codes on your pc

- ...More fearures are in progress

### Roadmap

Here are our milestones:
- [M1](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/1)
- [M2](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/2)
- [M3](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/3)

### MockUp

// comming

## Video Upload

### Select a video

There are two options to select a video:
 1. You can easily drop a video in the the "DragAndDrop" field
 2. Press the button "Select video" and choose your file

 Here is a list of supported video-formates:
 - .mp4

### Video uploading

- Press the Button "Start"
- The single stepps will be displayed under the "Start" button
- After finishing all stepps the HTML will be displayed
- You can copy the HTML-Code with the Button "copy to clipboard" 
- The Button "copy video-url" gives the link to your video

Notice:
If you try to upload a video with an empty config you will get an error.
Make sure that you *save* the config before you try the upload again :)


## Config

The JSON config file has the following format:

```
{
  "CfToken": "exampleToken",
  "CfAccount": "exampleVideoId",
  "KeyId": "exampleKeyId",
  "PrivateKey": "examplePrivateKey",
  "ExpiresIn": 0,
  "IsDarkmode": false
}
```
The Config is located in: 
```
C:\Users\LOCAL_USER\AppData\Roaming\CfStreamUploader\Config.json 
```
(make sure to exchange "LOCAL_USER" with you local windows user name ;) )


## HTML-Output

### Default HTML-Output

If you dont have an own HTML-Layout the CF-Stream-Uploader will generate a default HTML-Layout.
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

There again you can copie your own Html-Layout named "HtmlLayout.txt" in:
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