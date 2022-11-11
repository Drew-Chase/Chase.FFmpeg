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