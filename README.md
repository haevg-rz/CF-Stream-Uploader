# CF-Stream-Uploader

[![.github/workflows/build.yml](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml/badge.svg)](https://github.com/haevg-rz/CF-Stream-Uploader/actions/workflows/build.yml)


The CF-Stream-Uploader is an UI to upload your videos :video_camera: in your [cloudflare](https://www.cloudflare.com/de-de/) cloud.


### :speech_balloon: Features 

- You can easily upload your videos in your own [cloudflare](https://www.cloudflare.com/de-de/) cloud :cloud:
- Additional you can set restrictions for all your videos :unlock:
- The CF-Stream-Uploader will genereate a HTML-Code with your embedded videolink :page_facing_up:
- You can personalize the HTML-Code :pencil2:
- The CF-Stream-Uploader displays your video-storage-capacity for an easy overview :part_alternation_mark:
- The CF-Stream-Uploader saves your HTML-Codes on your pc :computer:
- ...
- More fearures are in progress :hourglass: :building_construction:

### :airplane: Roadmap 

Here are our milestones:
- [M1](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/1) The user can upload a given video and gets generated HTML Code to embedded in websites. :heavy_check_mark:
- [M2](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/2) The user can apply video restrictions and the complete process is visualised :building_construction:
- [M3](https://github.com/haevg-rz/CF-Stream-Uploader/milestone/3) The user can manage history and settings and can view remaining video runtime capacity

### :1234: MockUp 

Thats our first idea :stuck_out_tongue_winking_eye:

![Unbenannt](https://user-images.githubusercontent.com/62097375/118947701-e3cba700-b957-11eb-9422-2e9b7ca0c986.PNG)

## :chart_with_upwards_trend: Video Upload 

### :point_up: Select a video 

There are two options to select a video:
 1. You can easily drop a video in the the "DragAndDrop" field
 2. Press the button "Select video" and choose your file

 Here is a list of supported video-formates:
 - .mp4

### :movie_camera: Video uploading 

- Press the Button "Start"
- The single stepps will be displayed under the "Start" button
- After finishing all stepps the HTML will be displayed
- You can copy the HTML-Code with the Button "copy to clipboard" 
- The Button "copy video-url" gives the link to your video

:bangbang: Notice:
- If you try to upload a video with an empty config you will get an error.
- Make sure that you *save* the config before you try the upload again :)

## :pencil: Set restrictions
![image](https://user-images.githubusercontent.com/62097375/119634127-b331b480-be12-11eb-91d7-01a1055a4172.png)
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
- ...comming :building_construction:
### :black_nib: Default action
- May be used as a wildcard to apply a default action after other rules
- You can swap between "allow" and "block"
- Example: "all block" and "DE allow" --> the video will only accessible in Germany
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
