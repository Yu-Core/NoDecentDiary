﻿using SwashbucklerDiary.Shared;

namespace SwashbucklerDiary.Rcl.Repository
{
    public interface IUserStateModelRepository : IBaseRepository<UserStateModel>
    {
        Task<UserStateModel> InsertOrUpdateAsync(Achievement achievement);

        Task<UserStateModel> InsertOrUpdateAsync(Achievement achievement, int count);
    }
}
