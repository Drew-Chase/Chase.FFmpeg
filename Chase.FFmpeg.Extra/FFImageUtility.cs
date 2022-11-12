namespace Chase.FFmpeg.Extra;
/// <summary>
/// Static methods for handling image files
/// </summary>
public static class FFImageUtility
{
    /// <summary>
    /// Returns an array of all image file extensions
    /// </summary>
    public static readonly string[] image_extension = { "jxl", "bif", "mnr", "wbc", "sprite2", "icon", "afphoto", "xpm", "lrpreview", "pxd", "ase", "psd", "gif", "ysp", "snagx", "ptex", "psdc", "avatar", "flif", "png", "sumo", "bpg", "sprite", "sprite3", "tga", "hdr", "spr", "tpf", "jpeg", "piskel", "sai", "jps", "ct", "dds", "vicar", "clip", "pixela", "ipick", "avifs", "ppp", "lzp", "skitch", "oc4", "ff", "dib", "tbn", "aps", "accountpicture-ms", "qoi", "linea", "sld", "drp", "webp", "pcx", "jpg", "lip", "heif", "tfc", "itc2", "pm", "pdn", "exr", "wbz", "fits", "ktx", "psdx", "xcf", "djvu", "ota", "cpc", "cdc", "kra", "ppf", "usertile-ms", "tiff", "ozj", "pat", "bmp", "mpf", "pwp", "2bp", "msp", "pov", "tm2", "jpc", "rgf", "snag", "ecw", "icn", "nol", "pi2", "spp", "pdd", "pmg", "arr", "pic", "pfi", "psp", "73i", "kfx", "pnc", "gro", "vna", "pni", "wic", "nlm", "drz", "cmr", "mng", "px", "mdp", "pspimage", "stex", "cdg", "tn", "vrimg", "tg4", "i3d", "jpf", "vpe", "fil", "rsr", "fac", "fpx", "rpf", "iwi", "apng", "pbm", "heic", "jpe", "mdp", "sr", "jng", "psb", "aseprite", "awd", "bmq", "j2k", "ptg", "ppm", "pgm", "cpt", "dgt", "tif", "sph", "jbig2", "ljp", "bmz", "zif", "wbm", "jls", "gmbck", "ggr", "viff", "vrphoto", "pp5", "ozt", "g3n", "cals", "cpd", "wb0", "otb", "8ci", "ktx2", "can", "lmnr", "pam", "pse", "art", "pns", "wb2", "can", "thm", "mpo", "001", "jxr", "pic", "pgf", "mcs", "sig", "hdp", "wbmp", "jif", "sid", "xbm", "ce", "hif", "bti", "pe4", "pictclipping", "lbm", "cimg", "wdp", "pxd", "procreate", "info", "int", "pjpg", "djv", "abm", "tif", "ilbm", "lif", "rcl", "jbf", "kdi", "mbm", "ipv", "oc3", "pnt", "qtif", "oti", "vda", "apd", "pzs", "oci", "agp", "tex", "ufo", "jpg2", "hf", "vss", "jpg_large", "rtl", "rif", "jpx", "sup", "jp2", "spa", "sig", "gim", "pvr", "dtw", "awd", "jia", "wb1", "s2mv", "prw", "rli", "ais", "gp4", "ithmb", "qmg", "face", "thumb", "wi", "pxm", "neo", "v", "hpi", "dcm", "ncd", "bmx", "bmc", "8ca", "snagproj", "ica", "sai2", "pcd", "sun", "riff", "insp", "kodak", "cid", "sdr", "targa", "wmp", "max", "sar", "kic", "taac", "gpd", "pc1", "sff", "dpx", "pop", "urt", "pp4", "spiff", "hrf", "qti", "wpb", "spj", "dic", "pict", "rle", "art", "fppx", "psxprj", "sfc", "sktz", "j2c", "mix", "skm", "t2b", "texture", "afx", "picnc", "srf", "gmspr", "nwm", "gcdp", "mbm", "ozb", "rgb", "cd5", "svslide", "fsthumb", "pzp", "wbp", "lb", "avb", "bm2", "pza", "spe", "oplc", "gih", "dmi", "hdrp", "gbr", "oc5", "pixadex", "myl", "apx", "fpos", "jbig", "pjp", "tjp", "agif", "thm", "cin", "pxr", "msk", "zif", "zvi", "pxz", "avif", "psf", "ora", "dcx", "9.png", "jfi", "pspbrush", "cut", "sva", "dt2", "sgd", "ras", "pano", "acorn", "ddt", "xwd", "pyxel", "ncr", "360", "bss", "sim", "u", "cpg", "gfie", "mac", "dicom", "jtf", "cal", "fal", "jpd", "rcu", "jb2", "cit", "pap", "odi", "shg", "cam", "rs", "skypeemoticonset", "sfw", "mipmaps", "aic", "qif", "pac", "tub", "ipx", "miff", "bw", "mxi", "oe6", "fpg", "bs", "jbr", "epp", "ndpi", "sob", "sbp", "scn", "uga", "pjpeg", "tps", "jfif", "jiff", "ink", "pct", "jwl", "mrb", "ugoira", "ivr", "sct", "mat", "gfb", "ddb", "wvl", "hr", "ptk", "ptx", "8xi", "pov", "rsb", "smp", "tpi", "pntg", "sep", "rvg", "sgi", "omf", "rri", "jbg", "pnm", "y", "csf", "yuv", "dm3", "colz", "mip", "pbs", "suniff", "tn2", "rgba", "ldoc", "pic", "vmu", "ric", "g3f", "jas", "pc3", "fax", "tsr", "mic", "pts", "psdb", "svs", "pal", "pfr", "pxicon", "palm", "dc2", "brn", "icpr", "vic", "nct", "vdoc", "six", "dm4", "acr", "pe4", "1sc", "dc6", "wbd", "npsd", "xface", "sid", "t2k", "oir", "gvrs", "upf", "cpx", "c4", "frm", "dvl", "mrxs", "scn", "cpbitmap", "vst", "cps", "kpg", "scn", "ptx", "pix", "brt", "wpe", "bmf", "rgb", "trif", "ic1", "iphotoproject", "pix", "ic2", "ic3", "jbmp", "cin", "blkrt", "ivue", };

    /// <summary>
    /// Gets all files with image extension in specified directory
    /// </summary>
    /// <param name="path">The starting path</param>
    /// <param name="recursive">If the search should look through all subdirectories</param>
    /// <returns></returns>
    public static IReadOnlyCollection<string> GetFiles(string path, bool recursive = false) => FFDirectoryUtility.GetFiles(path, recursive, item => HasImageExtension(item));

    /// <summary>
    /// Checks if file has a extension matching the <seealso cref="image_extension">Image Extensions</seealso> array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool HasImageExtension(string path) => image_extension.Contains(new FileInfo(path).Extension.Trim('.'));
}
