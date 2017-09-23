# ZJUWLAN_AutoConnection
A toy-program written in C# to connect ZJUWLAN

//这是一个非常随性的readme

一、你可能会遇到的问题

Q：连不上网？

A：请检查以下几项：

①电脑是否打开了无线网卡？（理论上C#可以做到自动识别、开启无线网卡，但需要系统API，我懒得查怎么实现了...正常人也不会禁用无线网卡吧...）

②是否已经连接了有线网？（下个版本我会试图写一下自动连接有线网...但最近时间不够）

③用户名和密码是否正确？（浙大的密码多且不好记，初始密码应该是身份证后六位）

④是不是ZJUWLAN信号太弱了？

如果这几项都没问题，希望你联系我反馈：1176827825@qq.com

Q：程序崩溃了/卡住了？

A：希望你联系我反馈：1176827825@qq.com

Q：exe文件双击没反应

A：这个最可能的情况是你在设置中同时勾选了“打开后自动连接”和“连接后自动关闭”。这两个设置的组合有一个非常炫酷的正面效果和一个非常尴尬的负面效果。

正面效果是真正实现了connection in a flash，连接速度飞快且很省心。负面效果是你会觉得双击没反应（开启→急速连接成功→关闭），而且无法修改设置。针对这种情况，你可以删除文件夹中的“config.ini”文件并重新配置。

Q：刚开机时马上运行程序、有概率连不上

A：这是目前为止最让我头疼的一个bug，因为它切实的影响了用户体验（毕竟刚开机马上连wifi是刚需）。但目前来看，越来越多的信息指向这是一个Windows系统本身的问题。当然也不排除以后哪天我突然在代码里发现是自己又犯傻了。

这个bug的解决方案主要就是...在界面里点击重连。一般都连得上。

二、关于作者

竺可桢学院16级·计算机科学与技术·史嘉宁

联系方式上边都有


三、关于本程序

用C#写的，就是练练手。

本程序已在Github开源。若有意愿查看源代码、了解ZJUWLAN的连接机制，地址是：https://github.com/sjn4048/ZJUWLAN_AutoConnection

如果有愿意帮忙Debug & Pull Request的，在此先谢过了。（真的有这种好心人吗...）


四、在接下来的版本中...

1.支持有线网自动连接（这个特性应该比较exciting，目标是速度比浙大的连接器快2倍）

2.修bug、重构、优化代码逻辑、继续提速（现有的代码写的非常垃圾）

3.加入更多设置（嗅探自连接，etc...）

4.解决一下同时勾选“打开自动连接”和“连接后自动关闭”产生的劣质用户体验

5.自动连接系统WLAN

6.在更新时突发奇想的其他功能

