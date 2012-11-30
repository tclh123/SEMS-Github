#学院综合测评管理系统(SEMS)

http://sems.sssta.co

*原项目位于 http://sems.codeplex.com/*

##应用目的
* 提供一个高效的综合测评成绩管理解决方案
* 提供学生综合测评成绩的公示
* 可以完整保存往年的各个综合测评记录

##技术选型
* 开发平台：Windows 7/dotNet 4
* 后端语言：C#
* 框架：ASP.NET 4 MVC3 + Razor页面引擎
* 数据库: MySql + Entity Framework 4.1 (ORM) Code First模式
* Web前端：Jquery 1.5.1
* 第三方库：PagedList 1.14（C#）

##文档结构
* BLL（事务逻辑层）- 一些cs类，实现主要的业务逻辑
* Models（模型）- 数据库实体表类
* Controllers（控制器）- 类方法对应URI
* Views（展示层）- 一些 .cshtml文件，使用Razor页面引擎
* ViewModels - 从Model中剖离出来提供给View，用于Model不足以描述View的情况
* DAL（数据访问层）-对数据库进行持久化
* DB（建库sql脚本、数据库设计文档）
* Filters（一些validater，做权限验证）
* Content（css、img等）
* Uploads（上传文件目录）