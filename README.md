# Chase.FFmpeg

## Using Statements
```csharp
using Chase.FFmpeg.Converters;
using Chase.FFmpeg.Info;
using Chase.FFmpeg.Events;
using Chase.FFmpeg;
```

## Create media info object
```csharp
MediaInfo info = new MediaInfo("/path/to/media.mkv");
```

## Create Muxed converter
```csharp
MuxedConverter converter = MuxedConverter.SetMedia(info)
```
# Muxed converter options
```csharp
/// Changes the video codec
.ChangeVideoCodec("h264_nvenc")
```
## Changes the video bitrate
```csharp
.ChangeVideoBitrate("2M")
```
## Changes the audio codec
```csharp
.ChangeAudioCodec("aac")
```
## Changes the audio bitrate
```csharp
.ChangeAudioBitrate("200K")
```
## Changes the hardware acceleration method
```csharp
.ChangeHardwareAccelerationMethod()
```
## Changes the pixel format of the video
```csharp
.ChangePixelFormat("y2k")
```
## Changes the width and height of the video stream
```csharp
.ChangeResolution(800, 600)
```
## Changes the width of the video while maintaining aspect ratio
```csharp
.ChangeWidth(800)
```
## Changes the height of the video while maintaining aspect ratio
```csharp
.ChangeHeight(600)
```
## The position that the video starts at
```csharp
.ChangeStartPosition("00:00:00")
```
## Changes the duration of the video
```csharp
.ChangeVideoDuration("00:15:00")
```
## Overwrites output file if one exists
```csharp
.OverwriteOriginal();
```

## Events
```csharp
/// The console output of ffmpeg
DataReceivedEventHandler? data_handler = (object s, DataReceivedEventArgs e) => { };

/// Runs when ffmpeg exists
EventHandler exited = (object sender, EventArgs e) => { };

/// Returns ffmpeg update values
EventHandler<FFProcessUpdateEventArgs> updated = (object sender, FFProcessUpdateEventArgs e) =>
{
    /// The bitrate that the video is being processed at
    float bitrate = e.AverageBitrate;

    /// The number of frames already processed
    uint frames = e.FramesProcessed;

    /// The percentage of the video has been processed
    float percentage = e.Percentage;

    /// The speed that the video is processing at
    float speed = e.Speed;
};
```

## Converts the current Muxed Converter
```csharp
.Convert("/path/to/output.mkv", data_handler, exited, updated);
```

## Builds a ffmpeg argument string
```csharp
.Build("/path/to/output.mkv")
```

## Example
```csharp
string argument = MuxedConverter
    .SetMedia(info)
    .ChangeVideoCodec("h264_nvenc")
    .ChangeVideoBitrate("2M")
    .ChangeHardwareAccelerationMethod()
    .ChangeResolution(800, 600)
    .OverwriteOriginal()
    .Build("/path/to/output.mkv");
```

```csharp
FFProcessHandler.ExecuteFFmpeg(argument, data_handler, exited, updated);
```

# Chase.FFmpeg.Downloader 💽

This downloads the ffmpeg library.

## Using Statement
```csharp
using Chase.FFmpeg.Downloader;
```
## Download Latest Version
```csharp
/// Downloads the latest version of FFmpeg
FFmpegDownloader.Instance.GetLatest("/path/to/ffmpeg").Wait(); // Async Process
```

## Location of the FFmpeg and FFProbe executable files
```csharp
/// Path to ffmpeg executable
string ffmpeg = FFmpegDownloader.Instance.FFmpegExecutable;  // path/to/ffmpeg/ffmpeg.exe
/// Path to ffprobe executable
string ffprobe = FFmpegDownloader.Instance.FFprobeExecutable;  // path/to/ffmpeg/ffprobe.exe
```


## The installed version of ffmpeg library
```csharp
/// The installed ffmpeg version
string version = FFmpegDownloader.Instance.FFmpegVersion; // 4.0.1
```