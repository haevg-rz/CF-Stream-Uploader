# CF-Stream-Uploader

[![.github/workflows/build.yml](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml/badge.svg)](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml) [![.NET Publish](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/publish.yml/badge.svg)](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/publish.yml)


The CF-Stream-Uploader is an UI to upload your videos :video_camera: in your [cloudflare](https://www.cloudflare.com/de-de/) cloud.


### :speech_balloon: Features 

- You can easily upload your videos in your own [cloudflare](https://www.cloudflare.com/de-de/) cloud :cloud:
- Additional you can set restrictions for all your videos :unlock:
- The CF-Stream-Uploader will genereate a HTML-Code with your embedded videolink :page_facing_up:
- You can personalize the HTML-Code :pencil2:
- The CF-Stream-Uploader displays your video-storage-capacity for an easy overview :part_alternation_mark:
- The CF-Stream-Uploader saves your HTML-Codes on your pc :computer:
- ...

### :airplane: Roadmap 

Here are our milestones:
- [M1](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/1) The user can upload a given video and gets generated HTML Code to embedded in websites. :heavy_check_mark:
- [M2](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/2) The user can apply video restrictions and the complete process is visualised :heavy_check_mark:
- [M3](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/3) The user can manage history and settings and can view remaining video runtime capacity
:heavy_check_mark:

### 📱 MockUp 

<img src="https://user-images.githubusercontent.com/62097365/122367519-cab91480-cf5c-11eb-9116-218d0e9b9618.png" width="50%" height="50%">

## :chart_with_upwards_trend: Video Upload 

### :card_file_box: Open Settings

- Here you have to enter your account data
- All fields have to filled out
- Save your settings

<img src="https://user-images.githubusercontent.com/62097375/122385628-563aa180-cf6d-11eb-8d88-ce7272699eb9.png" width="50%" height="50%">
<img src="https://user-images.githubusercontent.com/62097375/122386398-23dd7400-cf6e-11eb-8be2-b5e8f9272219.png" width="40%" height="40%">

### :point_up: Select a video 

There are two options to select a video:
 1. You can easily drop a video in the the "DragAndDrop" field
 2. Press the button "Select video" and choose your file

The name of the video is now displayed bellow the drag and drop field

<img src="https://user-images.githubusercontent.com/62097375/122385532-3a370000-cf6d-11eb-8998-065f62d1c034.png" width="50%" height="50%">

 Here is a list of supported video-formates:
 - MP4, 
 - MKV
 - MOV
 - AVI
 - FLV
 - MPEG-2 TS
 - MPEG-2 PS
 - MXF
 - LXF
 - GXF
 - 3GP
 - WebM
 - MPG
 - QuickTime.

## :pencil: Set restrictions
<img src="https://user-images.githubusercontent.com/62097375/122385770-7bc7ab00-cf6d-11eb-8ee1-ff2243791cd0.png" width="50%" height="50%">

### :bangbang: These Rule actions are available
- "allow" - View is considered valid
- "block" - View is considered invalid and a 401 or 403 is returned
### :black_nib: Enter IP
- You can enter your IpAdresses in the textbox
- Each adress seperated with a comma
- Match specific IPv4 or IPV6 addresses or CIDRs
- It is recommended to include both IPv4 and IPv6 variants in a rule if possible
- The IpAdesses will have a simple validation check
- You can switch between "allow" or "block" for your entered IpAdresses
### :black_nib: Select country
- You can enter your Countries in the textbox
- Each coutry seperated with a comma 
- Match specific 2-letter country codes in [ISO 3166-1 Alpha-2](https://en.wikipedia.org/wiki/ISO_3166-1#Current_codes) format
- You have a list of supported countries in the link below
- You can switch between "allow" and "block" for your entered countries
### :black_nib: Set Access limit
- Set a time limit until the video can be seen
- The default value time is ten years (if it was not set by yourself)

### :movie_camera: Video uploading 

- Press the Button "Start"
- The single stepps will be displayed under the "Start" button
- After finishing all stepps the HTML will be displayed
- You can copy the HTML-Code with the button "copy HTML"
- The button "copy video-url" gives the link to your video

<img src="https://user-images.githubusercontent.com/62097375/122386606-56876c80-cf6e-11eb-8bd9-288e0d64f76b.png" width="50%" height="50%">

## :clipboard: Config 

The JSON config file has the following format:

```
{
  "UserSettings": {
    "CfToken": "exampleCfToken",
    "CfAccount": "exampleCfAccount",
    "KeyId": "exampleKeyId",
    "PrivateKey": "examplePrivateKey"
  },
  "AccessRules": {
    "Any": {
      "Action": "block",
      "Type": "any"
    },
    "Ip": {
      "Action": "allow",
      "Type": "ip.src",
      "Ips": [
        "127.0.0.1"
      ]
    },
    "Country": {
      "Action": "block",
      "Type": "ip.geoip.country",
      "Countries": [
        "DE"
      ]
    },
      "ExpiresIn": 365
  },
  "IsDarkmode": false
}
```
The Config is located in: 
```
C:\Users\LOCAL_USER\AppData\Roaming\CfStreamUploader\Config.json 
```
(make sure to exchange "LOCAL_USER" with you local windows user name ;) )

## Open history
- every video upload is saved
<img src="https://user-images.githubusercontent.com/62097375/122386279-090aff80-cf6e-11eb-8ab8-29ac76d5e7f8.png" width="50%" height="50%">

```
{
  "VideoTitle": "",
  "UploadDate": "",
  "VideoUrl": "",
  "SetAccesRules": [ ],
  "VideoToken": "",
  "HtmlCode": ""
}
```
## :newspaper: HTML-Output

### :file_folder: Default HTML-Output

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

### :file_folder: Personalized HTML-Output

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
