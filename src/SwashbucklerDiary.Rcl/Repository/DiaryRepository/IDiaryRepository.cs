﻿using SwashbucklerDiary.Shared;

namespace SwashbucklerDiary.Rcl.Repository
{
    public interface IDiaryRepository : IBaseRepository<DiaryModel>
    {
        Task<bool> UpdateTagsAsync(DiaryModel model);

        Task<List<TagModel>> GetTagsAsync(Guid id);

        Task<bool> UpdateIncludesAsync(DiaryModel model);

        Task<bool> UpdateIncludesAsync(List<DiaryModel> models);

        Task<bool> ImportAsync(List<DiaryModel> diaries);
    }
}
