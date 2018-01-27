# ZJUWLAN_AutoConnection
A toy-program written in C# to connect ZJUWLAN

# 18/01/27更新日志：(Version 1.2)
呼...终于考完试了，本次的主要更新内容：

1.增加了切换网络信号后的自动连接功能，实用性显著提升。现在程序已经几乎可以在任何时候帮你完成联网啦

2.增加了任务栏图标右键的功能，现在你可以直接在其中进入设置啦

3.增加了设置中关于“右上角叉号是关闭程序还是后台常驻的选项”

4.修复了连接其他wifi时不会显示信号的bug

5.优化了“后台常驻”的实现方法，现在在后台状态不会弹出请求失败的对话框啦


6.进一步优化了联网代码逻辑

# 18/01/25更新日志：(Version 1.1)
由于实在复习不下去DS，所以跑来更新一波，主要更新内容：

1.增加了任务栏图标的设定，软件可以常驻后台啦（几乎不消耗系统资源）

2.增加了从休眠/睡眠中唤醒后的自动连接，程序可用性大大提升

3.一定程度改善了偶发连不上网的情况。是否彻底解决仍有待观察（应该是没有，所以还要再改）

4.进行了一次小的重构

5.更换了新的图标

# 一、你可能会遇到的问题

Q：连不上网，弹出提示显示连接失败？

A：请检查以下几项：

由于Windows系统本身连接WLAN就不太稳定，因此为了排除Windows抽风，请先重试。如果重试后仍无法连接，请检查以下内容：

①电脑是否打开了无线网卡？（理论上C#可以做到自动识别、开启无线网卡，但需要系统API，我懒得查怎么实现了...正常人也不会禁用无线网卡吧...下个版本优化）

②是否已经连接了有线网？（下个版本我会试图写一下自动连接有线网...但最近时间不够）

③用户名和密码是否正确？（浙大的密码多且不好记，初始密码应该是身份证后六位）

④是不是ZJUWLAN信号太弱了？

如果这几项都没问题，希望你联系我反馈：1176827825@qq.com


Q：程序崩溃了/卡住了？

A：希望你联系我反馈：1176827825@qq.com


Q：刚开机时马上运行程序、有概率连不上

A：这是目前为止最让我头疼的一个bug，因为它切实的影响了用户体验（毕竟刚开机马上连wifi是刚需）。最近的几次重构优化了连接的实现思路，但是还是会存在这种问题。目前来看，越来越多的信息指向这是一个Windows系统本身的问题。当然也不排除以后哪天我突然在代码里发现是自己又犯傻了。这个bug的解决方案主要就是...重试一两次。一般都连得上。

# 二、关于作者

竺可桢学院16级·计算机科学与技术·史嘉宁。联系方式如上。


# 三、关于本程序

用C#写的，就是练练手。写着写着发现还有点实用性，就日常用着了

本程序已在Github开源。若有意愿查看源代码、了解ZJUWLAN的连接机制，地址是：https://github.com/sjn4048/ZJUWLAN_AutoConnection

如果有愿意帮忙Debug & Pull Request的，在此先谢过了。（如果真的有这种好心人吗）


# 四、在接下来的版本中...

1.支持有线网自动连接

2.修bug、重构、优化代码逻辑、继续提速（长期工程）

3.自动启动系统网卡、开启wifi开关

4.在线更新程序功能（有了这个功能之后就可以发布了）

5.在更新时突发奇想的其他功能