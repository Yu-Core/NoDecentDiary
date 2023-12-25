﻿namespace SwashbucklerDiary.Maui.Essentials
{
    public partial class PlatformIntegration
    {
        public async Task<bool> CheckCameraPermission()
        {
            return await CheckPermission<Permissions.Camera>();
        }

        public async Task<bool> TryCameraPermission()
        {
            if (!IsCaptureSupported())
            {
                await _alertService.Error(_i18n.T("User.NoCapture"));
                return false;
            }

            return await TryPermission<Permissions.Camera>("Permission.OpenCamera");
        }
    }
}
