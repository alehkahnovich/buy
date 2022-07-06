﻿using System;

namespace Buy.Rasterization.DataAccess.Domain
{
    public sealed class Artifact {
        public int ArtifactId { get; set; }
        public Guid UploadKey { get; set; }
        public int RequestId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SortOrder { get; set; }
    }
}