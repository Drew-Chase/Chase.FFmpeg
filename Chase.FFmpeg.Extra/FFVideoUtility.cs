
namespace Chase.FFmpeg.Extra;

/// <summary>
/// Static methods for handling video files
/// </summary>
public static class FFVideoUtility
{
    /// <summary>
    /// Returns an array of all video file extensions
    /// </summary>
    public static readonly string[] video_extension = { "STR", "SWF", "AEP", "MKV", "PZ", "PLOT", "SFD", "KINE", "DRP", "PSV", "PRPROJ", "PIV", "PLOTDOC", "INP", "CPVC", "PIC", "ANM", "VPROJ", "VEG", "WLMP", "MSDVD", "AEC", "BIK", "KDENLIVE", "MSWMM", "DCR", "WEBM", "AMC", "MP5", "SCM", "VPJ", "WPL", "DIR", "MP4", "DCR", "EVO", "FCP", "FBR", "SRT", "MPV", "VOB", "MSE", "RMVB", "FLC", "CLPI", "SBT", "SUB", "VSP", "PAC", "3GP", "IFO", "MXF", "DMSM", "CINE", "REC", "CAMPROJ", "MVD", "IVR", "DMX", "VP6", "MPEG", "WMMP", "ALPX", "AV1", "DPA", "M4U", "264", "PSH", "META", "VTT", "TSV", "TRP", "RCD", "GTS", "SWI", "DB2", "AEPX", "MVP", "MGV", "AMX", "MANI", "D3V", "SCREENFLOW", "WMV", "HDMOV", "RMS", "VIDEO", "VIV", "3GP2", "ISMV", "OGV", "ASF", "VC1", "FLV", "TVSHOW", "MYS", "GFP", "DMSM3D", "SIV", "ALE", "ARCUT", "MEPS", "PMF", "M2TS", "SER", "RCUT", "ZM2", "MPSUB", "G2M", "NCOR", "JTV", "DXR", "DV4", "MPROJ", "MP4V", "MP4.INFOVID", "TS", "TREC", "DAT", "M1V", "CME", "IDX", "ZMV", "SCC", "MJ2", "M2T", "DREAM", "IVA", "RM", "M4V", "MPEG4", "DZM", "CAMREC", "MJPG", "SMV", "THEATER", "TIX", "MEPX", "BK2", "MNV", "WP3", "STX", "VRO", "MPG", "F4P", "AWLIVE", "XMV", "MOV", "WVM", "VGZ", "DZP", "DV", "TP", "MTS", "PDS", "PPJ", "VII", "AVV", "M4S", "CST", "DVR", "MMP", "TMV", "DMB", "QTCH", "DZT", "IRCP", "SEDPRJ", "FBR", "FFD", "CAMV", "KTN", "DVR-MS", "MV", "MEP", "DASH", "FLI", "KMPROJECT", "IZZ", "AVB", "SFVIDCAP", "INT", "ZM3", "IZZY", "VID", "264", "BNP", "DAV", "H264", "WVX", "PLAYLIST", "AVCHD", "3MM", "CPI", "SBK", "VEP", "3G2", "MP2V", "XESC", "MVP", "RDB", "SFERA", "BSF", "JDR", "60D", "XVID", "LVIX", "DDAT", "D2V", "890", "EXI", "SAN", "PGI", "YUV", "AAF", "G64X", "ISM", "G64", "MEDIA", "F4V", "MOVIE", "3GPP2", "MPL", "3GPP", "JSS", "R3D", "XLMV", "QTL", "SQZ", "MVEX", "M4F", "HDV", "F4F", "MP21", "MK3D", "TBC", "RSX", "PRO", "PDRPROJ", "DCK", "DNC", "TSP", "VCR", "BDMV", "DIVX", "HEVC", "OGM", "M2P", "K3G", "MOI", "WCP", "NUV", "SMK", "RV", "SWT", "XML", "SPL", "WM", "OGX", "AVI", "DPG", "BU", "CED", "DVDMEDIA", "RUM", "TVLAYER", "TDT", "LREC", "3P2", "WRF", "EXO", "MPE", "ARF", "BMC", "MTV", "BDT3", "VP7", "M2A", "TIVO", "WMD", "FLIC", "NFV", "TVS", "LSX", "LRV", "TPD", "MPG4", "MOOV", "RMP", "AETX", "JMV", "DMSD", "F4M", "ZOOM", "MJP", "PREL", "WXP", "MVC", "NVC", "ASX", "IMOVIEPROJ", "Y4M", "MOVIE", "BVR", "TTXT", "IRF", "AJP", "FTC", "PRTL", "WOT", "ORV", "AVD", "VSE", "AVS", "TVRECORDING", "AXV", "IMOVIELIBRARY", "MVE", "M4E", "AVE", "PLPROJ", "IVF", "CMMTPL", "RVID", "DVT", "LFPACKAGE", "TTML", "HKM", "SUB", "ISMC", "M2V", "PHOTOSHOW", "TDA3MT", "M21", "DVX", "TOD", "OTRKEY", "CMPROJ", "MPGINDEX", "ZM1", "FLH", "BMK", "EVO", "VDR", "QT", "VCV", "ROQ", "M21", "TPR", "PXV", "PEG", "RVL", "GXF", "BRAW", "IMOVIEMOBILE", "PROJECTOR", "SBZ", "BDM", "RCREC", "VR", "FPDX", "N3R", "VBC", "AVP", "SDV", "SMIL", "WTV", "INSV", "AEGRAPHIC", "MPL", "SEC", "WMX", "EDL", "VCPF", "PAR", "PNS", "YOG", "RMD", "W32", "QTM", "GIFV", "GCS", "AMV", "VIX", "XEL", "CLK", "CREC", "NSV", "AV", "THP", "PVR", "VFZ", "RAVI", "FCPROJECT", "PSSD", "DV-AVI", "SMI", "SSA", "VS4", "MPC", "NTP", "BS4", "BYU", "QSV", "FCARCH", "M1PG", "VFW", "VLAB", "BLZ", "SEQ", "PROQC", "MPLS", "MODD", "MXV", "DIF", "MOD", "VIEWLET", "QTZ", "TP0", "XFL", "FFM", "SPRYZIP", "DLX", "VDO", "CMMP", "IMOVIEPROJECT", "CMREC", "AET", "WSVE", "PRO4DVD", "JTS", "DMSS", "VF", "DAD", "VEM", "AECAP", "AXM", "EZT", "XEJ", "EXP", "USF", "STL", "SVI", "SMI", "DCE", "FBZ", "MOFF", "CMV", "CIP", "SCN", "AVM", "PVA", "H265", "EYETV", "MQV", "AVS", "WGI", "AVR", "RCPROJECT", "EYE", "MJPEG", "BIK2", "DRC", "CAMTEMPLATE", "WFSP", "SKM", "RTS", "EL8", "CAM", "AVS", "MVY", "PRO5DVD", "VSR", "DMSD3D", "PMP", "CX3", "VP5", "ANX", "BDT2", "AVC", "VMLT", "VMLF", "QTINDEX", "CDXL", "ANYDESK", "PCLX", "GVP", "MP21", "TID", "RP", "SML", "LVF", "FLX", "VSH", "H263", "JNR", "AV3", "VFT", "KUX", "RTS", };

    /// <summary>
    /// Checks if file has a extension matching the <seealso cref="video_extension">Video Extensions</seealso> array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool HasVideoExtension(string path) => video_extension.Contains(new FileInfo(path).Extension.Trim('.'));
    /// <summary>
    /// Gets all files with video extension in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static IReadOnlyCollection<string> GetFiles(string path, bool recursive = false) => FFDirectoryUtility.GetFiles(path, recursive, item => HasVideoExtension(item));
}
