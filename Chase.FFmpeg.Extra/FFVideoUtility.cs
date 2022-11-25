
using Chase.FFmpeg.Info;

namespace Chase.FFmpeg.Extra;

/// <summary>
/// Static methods for handling video files
/// </summary>
public static class FFVideoUtility
{
    /// <summary>
    /// Returns an array of all video file extensions
    /// </summary>
    public static readonly string[] video_extension = { "str", "swf", "aep", "mkv", "pz", "plot", "sfd", "kine", "drp", "psv", "prproj", "piv", "plotdoc", "inp", "cpvc", "pic", "anm", "veg", "wlmp", "msdvd", "aec", "bik", "kdenlive", "mswmm", "dcr", "webm", "amc", "mp5", "scm", "wpl", "dir", "mp4", "dcr", "evo", "fcp", "fbr", "mpv", "vob", "mse", "rmvb", "flc", "clpi", "sbt", "sub", "vsp", "pac", "3gp", "ifo", "mxf", "dmsm", "cine", "rec", "camproj", "mvd", "ivr", "dmx", "vp6", "mpeg", "wmmp", "alpx", "av1", "dpa", "m4u", "264", "psh", "meta", "vtt", "tsv", "trp", "rcd", "gts", "swi", "db2", "aepx", "mvp", "mgv", "amx", "mani", "d3v", "screenflow", "wmv", "hdmov", "rms", "video", "viv", "3gp2", "ismv", "ogv", "asf", "vc1", "flv", "tvshow", "mys", "gfp", "dmsm3d", "siv", "ale", "arcut", "meps", "pmf", "m2ts", "ser", "rcut", "zm2", "mpsub", "g2m", "ncor", "jtv", "dxr", "dv4", "mproj", "mp4v", "mp4.infovid", "ts", "trec", "dat", "m1v", "cme", "idx", "zmv", "scc", "mj2", "m2t", "dream", "iva", "rm", "m4v", "mpeg4", "dzm", "camrec", "mjpg", "smv", "theater", "tix", "mepx", "bk2", "mnv", "wp3", "stx", "vro", "mpg", "f4p", "awlive", "xmv", "mov", "wvm", "vgz", "dzp", "dv", "tp", "mts", "pds", "ppj", "vii", "avv", "m4s", "cst", "dvr", "mmp", "tmv", "dmb", "qtch", "dzt", "ircp", "sedprj", "fbr", "ffd", "camv", "ktn", "dvr-ms", "mv", "mep", "dash", "fli", "kmproject", "izz", "avb", "sfvidcap", "int", "zm3", "izzy", "vid", "264", "bnp", "dav", "h264", "wvx", "playlist", "avchd", "3mm", "cpi", "sbk", "vep", "3g2", "mp2v", "xesc", "mvp", "rdb", "sfera", "bsf", "jdr", "60d", "xvid", "lvix", "ddat", "d2v", "890", "exi", "san", "pgi", "yuv", "aaf", "g64x", "ism", "g64", "media", "f4v", "movie", "3gpp2", "mpl", "3gpp", "jss", "r3d", "xlmv", "qtl", "sqz", "mvex", "m4f", "hdv", "f4f", "mp21", "mk3d", "tbc", "rsx", "pro", "pdrproj", "dck", "dnc", "tsp", "vcr", "bdmv", "divx", "hevc", "ogm", "m2p", "k3g", "moi", "wcp", "nuv", "smk", "rv", "swt", "xml", "spl", "wm", "ogx", "avi", "dpg", "bu", "ced", "dvdmedia", "rum", "tvlayer", "tdt", "lrec", "3p2", "wrf", "exo", "mpe", "arf", "bmc", "mtv", "bdt3", "vp7", "m2a", "tivo", "wmd", "flic", "nfv", "tvs", "lsx", "lrv", "tpd", "mpg4", "moov", "rmp", "aetx", "jmv", "dmsd", "f4m", "zoom", "mjp", "prel", "wxp", "mvc", "nvc", "asx", "imovieproj", "y4m", "movie", "bvr", "ttxt", "irf", "ajp", "ftc", "prtl", "wot", "orv", "avd", "vse", "avs", "tvrecording", "axv", "imovielibrary", "mve", "m4e", "ave", "plproj", "ivf", "cmmtpl", "rvid", "dvt", "lfpackage", "ttml", "hkm", "sub", "ismc", "m2v", "photoshow", "tda3mt", "m21", "dvx", "tod", "otrkey", "cmproj", "mpgindex", "zm1", "flh", "bmk", "evo", "vdr", "qt", "vcv", "roq", "m21", "tpr", "pxv", "peg", "rvl", "gxf", "braw", "imoviemobile", "projector", "sbz", "bdm", "rcrec", "vr", "fpdx", "n3r", "vbc", "avp", "sdv", "smil", "wtv", "insv", "aegraphic", "mpl", "sec", "wmx", "edl", "vcpf", "par", "pns", "yog", "rmd", "w32", "qtm", "gifv", "gcs", "amv", "vix", "xel", "clk", "crec", "nsv", "av", "thp", "pvr", "vfz", "ravi", "fcproject", "pssd", "dv-avi", "smi", "ssa", "vs4", "mpc", "ntp", "bs4", "byu", "qsv", "fcarch", "m1pg", "vfw", "vlab", "blz", "seq", "proqc", "mpls", "modd", "mxv", "dif", "mod", "viewlet", "qtz", "tp0", "xfl", "ffm", "spryzip", "dlx", "vdo", "cmmp", "imovieproject", "cmrec", "aet", "wsve", "pro4dvd", "jts", "dmss", "vf", "dad", "vem", "aecap", "axm", "ezt", "xej", "exp", "usf", "stl", "svi", "smi", "dce", "fbz", "moff", "cmv", "cip", "scn", "avm", "pva", "h265", "eyetv", "mqv", "avs", "wgi", "avr", "rcproject", "eye", "mjpeg", "bik2", "drc", "camtemplate", "wfsp", "skm", "rts", "el8", "cam", "avs", "mvy", "pro5dvd", "vsr", "dmsd3d", "pmp", "cx3", "vp5", "anx", "bdt2", "avc", "vmlt", "vmlf", "qtindex", "cdxl", "anydesk", "pclx", "gvp", "mp21", "tid", "rp", "sml", "lvf", "flx", "vsh", "h263", "jnr", "av3", "vft", "kux", "rts", };

    /// <summary>
    /// Gets all files with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFiles(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFiles(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<FFMediaInfo> GetMediaFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetMediaFilesAsync(path, recursive, item => HasVideoExtension(item));
    /// <summary>
    /// Gets all files with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFiles(string path, bool recursive = false) => FFDirectoryUtility.GetFiles(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Gets all files asynchronous with <seealso cref="video_extension">Video Extension</seealso> in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static ICollection<string> GetFilesAsync(string path, bool recursive = false) => FFDirectoryUtility.GetFilesAsync(path, recursive, item => HasVideoExtension(item));

    /// <summary>
    /// Checks if file has a extension matching the <seealso cref="video_extension">Video Extensions</seealso> array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool HasVideoExtension(string path) => video_extension.Contains(new FileInfo(path).Extension.Trim('.'));
}
