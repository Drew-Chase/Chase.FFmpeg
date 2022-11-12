# Chase.FFmpeg.Extras

## Usings 
```csharp
using Chase.FFmpeg.Extras;
```

## Video
### Gets all files with video extension in specified directory
```csharp
/// <param name="path">The starting path</param>
/// <param name="recursive">If the search should look through all subdirectories</param>
FFVideoUtility.GetFiles("path/to/files", true)
```
### An array of all video file extensions
```csharp
FFVideoUtility.video_extensions
```

### Checks if file has a extension matching the [Video Extensions](#An-array-of-all-video-file-extensions) array
```csharp
FFVideoUtility.HasVideoExtension("/path/to/video.mkv")
```

## Audio
### Gets all files with audio extension in specified directory
```csharp
/// <param name="path">The starting path</param>
/// <param name="recursive">If the search should look through all subdirectories</param>
FFAudioUtility.GetFiles("path/to/files", true)
```
### An array of all audio file extensions
```csharp
FFAudioUtility.audio_extensions
```

### Checks if file has a extension matching the [Audio Extensions](#An-array-of-all-audio-file-extensions) array
```csharp
FFAudioUtility.HasAudioExtension("/path/to/audio.mp3")
```

## Images
### Gets all files with images extension in specified directory
```csharp
/// <param name="path">The starting path</param>
/// <param name="recursive">If the search should look through all subdirectories</param>
FFImagesUtility.GetFiles("path/to/files", true)
```
### An array of all images file extensions
```csharp
FFImagesUtility.images_extensions
```

### Checks if file has a extension matching the [Images Extensions](#An-array-of-all-images-file-extensions) array
```csharp
FFImagesUtility.HasImagesExtension("/path/to/images.mp3")
```