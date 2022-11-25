using Chase.FFmpeg.Info;

namespace Chase.FFmpeg.Extra;
/// <summary>
/// Static methods for handling audio files
/// </summary>
public static class FFAudioUtility
{
    /// <summary>
    /// Returns an array of all audio file extensions
    /// </summary>
    public static readonly string[] audio_extension = { "mtm", "ec3", "flp", "abc", "mp3", "l", "wproj", "weba", "copy", "mui", "ust", "flac", "asd", "sf2", "xfs", "aup", "mti", "fev", "sds", "cdo", "omg", "gp", "vpw", "pna", "cgrp", "voxal", "fsc", "amxd", "midi", "mmlp", "minigsf", "sty", "xrns", "sngx", "xmu", "kt3", "efs", "igp", "dct", "afc", "dmse", "mka", "slp", "h5s", "acp", "wow", "sc2", "cwb", "musicxml", "phy", "ftm", "aimppl", "toc", "mmpz", "itls", "ptxt", "gsf", "wus", "rad", "vsq", "nki", "saf", "dff", "m4r", "sfk", "als", "vdj", "gsm", "gp5", "mid", "4mp", "apl", "band", "bun", "cue", "ang", "pcg", "rmj", "ogg", "uax", "uni", "logic", "rns", "rx2", "vag", "akp", "g726", "sng", "abm", "wav", "acm", "rex", "5xe", "wax", "omf", "vlc", "alc", "sfpack", "mscz", "rip", "aria", "sgp", "ncw", "psm", "ovw", "emx", "cws", "dsm", "acd-zip", "pla", "sfl", "ac3", "pek", "iti", "vyf", "dm", "wfp", "dcf", "act", "frg", "qcp", "rmx", "sseq", "q1", "vsqx", "q2", "omx", "rol", "nrt", "vqf", "sesx", "m3u8", "flm", "m4a", "sdat", "gbs", "rgrp", "bww", "pts", "ptx", "aob", "wma", "s3i", "tg", "pandora", "aup3", "f32", "isma", "trak", "aa3", "mpu", "sbi", "pcast", "acd", "mxl", "ckb", "s3m", "cidb", "sib", "ram", "rso", "3ga", "at3", "gpk", "vgm", "w01", "med", "mtf", "oma", "aif", "wave", "m3u", "stm", "cdda", "mod", "note", "wrk", "logicx", "pkf", "h5b", "cwt", "uw", "b4s", "ins", "mtp", "m4b", "dra", "svd", "mux", "zpa", "kmp", "opus", "caf", "wpp", "cts", "669", "ptt", "wfm", "mus", "mus", "ics", "mo3", "xa", "aac", "cda", "xspf", "aiff", "rti", "vc3", "dmsa", "lof", "rta", "amf", "aa", "sd", "emd", "sou", "sxt", "mx5template", "wvc", "ptm", "vpl", "mx4", "mx3", "stap", "mdr", "mpdp", "iaa", "smf", "ds", "agm", "bdd", "swa", "bidule", "cdr", "h4b", "dig", "mbr", "wfb", "gpbank", "nkx", "ssnd", "dss", "a2p", "oga", "bnk", "nkm", "ftm", "cwp", "dsf", "mogg", "agr", "sng", "zvd", "vmd", "yookoo", "wpk", "vox", "f4a", "lso", "bwg", "aud", "dtshd", "bnl", "cpr", "brstm", "amr", "wve", "mxmf", "dcm", "iff", "gsflib", "ra", "nml", "w64", "drg", "rcy", "syw", "u", "sns", "m4p", "fpa", "wtpt", "syh", "vip", "raw", "nwc", "vpm", "nkc", "shn", "gpx", "mpa", "tak", "lwv", "igr", "mdc", "npl", "la", "pk", "ftmx", "dvf", "ab", "kar", "myr", "mx5", "ppcx", "uwf", "odm", "syn", "obw", "tak", "dewf", "h5e", "musx", "mmm", "rmi", "nvf", "dw", "xsp", "vpr", "ds2", "psf", "adt", "wv", "avastsounds", "mmp", "a2m", "mpga", "5xb", "all", "dts", "peak", "song", "cfa", "seq", "gbproj", "vap", "caff", "psf1", "nra", "psf2", "ssm", "ksc", "xm", "mt2", "vb", "pac", "zpl", "au", "groove", "rng", "hsb", "m5p", "fdp", "k26", "hca", "krz", "sma", "conform", "msmpl_bank", "slx", "sppack", "wwu", "dls", "hbe", "koz", "acd-bak", "koz", "wut", "ma1", "ots", "rsn", "nsa", "dpdoc", "rpl", "rcd", "esps", "pca", "rdvxz", "mpd", "mptm", "efq", "m3up", "bwf", "sfz", "aaxc", "svp", "efk", "capobundle", "fzf", "fzv", "gig", "g721", "scs11", "sap", "tta", "8svx", "mts", "rfl", "ksf", "ape", "pho", "jam", "its", "a2b", "sd", "adv", "amz", "ptf", "adg", "nmsv", "aifc", "pno", "usf", "smp", "miniusf", "csh", "ove", "xmf", "aax", "ams", "dmf", "ses", "vrf", "r1m", "mp2", "mpc", "ovw", "psy", "5xs", "rbs", "smpx", "dmc", "prg", "nks", "mmf", "f2r", "bank", "voc", "a2i", "nkb", "vgz", "stx", "s3z", "ppc", "emp", "rvx", "mgv", "ult", "smp", "usflib", "bap", "snd", "snd", "vtx", "exs", "g723", "sfap0", "td0", "adts", "f64", "kfn", "sd2f", "expressionmap", "rbs", "ams", "wfd", "sfs", "ckf", "minipsf2", "minipsf", "fsm", "svx", "dwd", "narrative", "mpa2", "pvc", "rax", "repeaks", "jspf", "ptcop", "ofr", "cpt", "snd", "wtpl", "a2t", "ariax", "ntn", "mte", "ay", "pbf", "txw", "vmo", "cel", "jbx", "sd2", "vmf", "rts", "msv", "vmf", };

    /// <summary>
    /// Gets all files with <seealso cref="audio_extension">Audio Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFiles(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFiles(path, recursive, item => HasAudioExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="audio_extension">Audio Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFilesAsync(path, recursive, item => HasAudioExtension(item));
    /// <summary>
    /// Gets all files with <seealso cref="audio_extension">Audio Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFiles(string path, bool recursive = false) => FFDirectoryUtility.GetFiles(path, recursive, item => HasAudioExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="audio_extension">Audio Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetFilesAsync(path, recursive, item => HasAudioExtension(item));

    /// <summary>
    /// Checks if file has a extension matching the <seealso cref="audio_extension">Audio Extensions</seealso> array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool HasAudioExtension(string path) => audio_extension.Contains(new FileInfo(path).Extension.Trim('.'));
}
