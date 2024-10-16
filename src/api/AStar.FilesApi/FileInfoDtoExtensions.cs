﻿using AStar.FilesApi.Models;

namespace AStar.FilesApi;

public static class FileInfoDtoExtensions
{
    public static bool IsImage(this FileInfoDto fileInfo) => fileInfo.Extension.ToUpperInvariant() is "JPEG"
            or "JPG"
            or "PNG"
            or "GIF"
            or "BMP";
}
