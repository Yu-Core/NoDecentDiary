﻿namespace SwashbucklerDiary.Shared
{
    public class DiaryModel : BaseModel
    {
        public string? Title { get; set; }

        public string? Content { get; set; }

        public string? Mood { get; set; }

        public string? Weather { get; set; }

        public string? Location { get; set; }

        public bool Top { get; set; }

        public bool Private { get; set; }

        public List<TagModel>? Tags { get; set; }
        
        public List<ResourceModel>? Resources { get; set; }
    }
}
