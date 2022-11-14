﻿using System.Collections.Specialized;

namespace BiliStart.Contracts.Services;

public interface IAppNotificationService
{
    void Initialize();

    bool Show(string payload);

    NameValueCollection ParseArguments(string arguments);

    void Unregister();

    void CreateShow(string arguments,string PrimaryText,string SecondaryText, string Title, string SubTitle,string LeftImage = "");
}
