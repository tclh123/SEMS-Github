-- synthetical evaluation management system (SEMS)
drop schema if exists `SEMSDB`;
CREATE SCHEMA `SEMSDB`;
use `SEMSDB`;

/* clean up old tables;
   must drop tables with foreign keys first
   due to referential integrity constraints
 */

drop table if exists news;
drop table if exists policy;
drop table if exists administrater;
drop table if exists sysinfo;
drop table if exists module_score;
drop table if exists entry_score;
drop table if exists evaluation_entry;
drop table if exists module;
drop table if exists lk_evaluation_year_classes;
drop table if exists evaluation_year;
drop table if exists student;
drop table if exists classes;

/* Create tables 
    表名称默认不区分大小写
    对应实体Model类，大写开头
*/

-- 班级表  class避免与关键字冲突，加复数
create table classes
   (class_id 	varchar(15)	not null comment'班级ID, e.g. 131013',    -- 班级ID, e.g. 131013
    class_small_id  varchar(5) not null comment'小班号（行政班），e.g. 01',
    class_grade		varchar(5) comment'班级所属年级（数据库里暂不处理年级）, e.g.2010',         -- 班级所属年级, e.g.2010
    primary key(class_id, class_small_id)) comment'班级表';

-- 学生表
create table student
   (student_id 	varchar(15)	not null unique comment'学号',    -- 学号
    student_name		nvarchar(15)	not null comment'姓名',         -- 姓名
    student_sex 		enum('男', '女') comment'性别',   -- 性别
    class_id 	varchar(15)	not null comment'班级ID, e.g. 131013',    -- 班级ID, e.g. 131013
    class_small_id  varchar(5) not null comment'小班号（行政班），e.g. 01',
    primary key(student_id),
    foreign key(class_id, class_small_id) references classes(class_id,class_small_id) on delete cascade on update cascade) comment'学生表';   -- 学生属于一个班级
    
-- 测评年表（静态表）
create table evaluation_year
   (evaluation_year_id 	int	not null auto_increment comment'测评年ID，e.g. 1',
    evaluation_year_name  nvarchar(15) not null comment'测评年名称，e.g. 大一第一学期、大一第二学期、大二全年、大三全年',
    primary key(evaluation_year_id)) comment'测评年表';
    
-- 测评年班级链接表（添加班级时同时添加其测评年，若存在记录，则表示此班级已经进行测评，同时指出测评学年学期）
create table lk_evaluation_year_classes
   (evaluation_year_id 	int	not null comment'测评年ID，e.g. 1',
    class_id 	varchar(15)	not null comment'班级ID, e.g. 131013',    -- 班级ID, e.g. 131013
    class_small_id  varchar(5) not null comment'小班号（行政班），e.g. 01',
    evaluation_school_year		varchar(15) not null comment'测评学年, e.g.2011-2012', 
    evaluation_semester   enum('秋', '春') not null comment'测评学期', 
    primary key(evaluation_year_id, class_id, class_small_id),
    foreign key(evaluation_year_id) references evaluation_year(evaluation_year_id), 
    foreign key(class_id, class_small_id) references classes(class_id, class_small_id) on delete cascade on update cascade) comment'测评年班级链接表（添加班级时同时添加其测评年，若存在记录，则表示此班级已经进行测评，同时指出测评学年学期）';    
    
-- 模块表
create table module
   (module_id 	varchar(5) not null unique comment'模块ID，e.g. M1',
    module_name  nvarchar(15) not null comment'模块名称，e.g. 思想道德素质模块',
    primary key(module_id)) comment'模块表';
    
-- 评测项目表
create table evaluation_entry
   (entry_id 	int	not null auto_increment comment'条目ID，int自增',
    entry_school_year		varchar(15) not null comment'举办学年, e.g.2011-2012', 
    entry_semester   enum('秋', '春') not null comment'学期', 
    entry_description nvarchar(150) not null comment'项目简介，100字内', 
    -- entry_date_str    nvarchar(15) not null comment'项目开始时间（字符串，e.g. 9月底）', 
    entry_date  date not null comment'项目开始时间，e.g. YYYY-MM-DD）', 
    module_id 	varchar(5) not null comment'模块ID，e.g. M1',  -- 所属模块
    primary key(entry_id),
    foreign key(module_id) references module(module_id) on delete cascade on update cascade) comment'评测项目表';
    
-- 项目得分表
create table entry_score
   (student_id 	varchar(15)	not null comment'学生学号',    -- 学生学号
    entry_id 	int	not null comment'条目ID',
    score   int not null comment'得分', 
    primary key(student_id, entry_id),
    foreign key(student_id) references student(student_id) on delete cascade on update cascade, 
    foreign key(entry_id) references evaluation_entry(entry_id) on delete cascade on update cascade) comment'项目得分表';    
    
-- 模块总分表
create table module_score
   (score_id int	not null auto_increment comment'总分ID，int自增',        -- 避免多字段联合主键
    student_id 	varchar(15)	not null comment'学生学号',    -- 学生学号
    module_id 	varchar(5) not null,
    score   int not null comment'得分', 
    evaluation_school_year		varchar(15) not null comment'测评学年, e.g.2011-2012', 
    evaluation_semester   enum('秋', '春') not null comment'测评学期', 
    primary key(score_id),
    unique(student_id, module_id, evaluation_school_year, evaluation_semester),     -- 采用联合UNIQUE保证数据唯一性
    foreign key(student_id) references student(student_id) on delete cascade on update cascade, 
    foreign key(module_id) references module(module_id) on delete cascade on update cascade) comment'模块总分表';
    
DELIMITER //

-- 触发器，维护总分表与子条目表中分数一致性
create trigger tr_entry_score_insert before insert on entry_score   -- 增
    for each row
    begin
        DECLARE $module_id varchar(5);
        DECLARE $school_year varchar(15);
        DECLARE $semester nvarchar(5);
        select module_id into $module_id from evaluation_entry where entry_id=new.entry_id; -- 插入的是哪个模块的
        select entry_school_year into $school_year from evaluation_entry where entry_id=new.entry_id; -- 插入的是哪个学年
        select entry_semester into $semester from evaluation_entry where entry_id=new.entry_id; -- 插入的是哪个学期   
        if not exists(select 1 from module_score where student_id=new.student_id and module_id=$module_id 
            and evaluation_school_year=$school_year and evaluation_semester=$semester)
        then insert module_score(student_id, module_id, score, evaluation_school_year, evaluation_semester) 
            values (new.student_id, $module_id, new.score,$school_year,$semester); -- 当不存在此人此模块的总分，则新建
        else update module_score set score=score+new.score where student_id=new.student_id and module_id=$module_id
            and evaluation_school_year=$school_year and evaluation_semester=$semester;  -- 已存在，则更新
        end if;
    end
//
create trigger tr_entry_score_update before update on entry_score   -- 改
    for each row
    begin
        DECLARE $module_id varchar(5);
        DECLARE $school_year varchar(15);
        DECLARE $semester nvarchar(5);
        select module_id into $module_id from evaluation_entry where entry_id=new.entry_id; -- 是哪个模块的
        select entry_school_year into $school_year from evaluation_entry where entry_id=new.entry_id; -- 是哪个学年
        select entry_semester into $semester from evaluation_entry where entry_id=new.entry_id; -- 是哪个学期   
        update module_score set score=score-old.score+new.score where student_id=new.student_id and module_id=$module_id
            and evaluation_school_year=$school_year and evaluation_semester=$semester;  -- 已存在，则更新
    end
//
create trigger tr_entry_score_delete before delete on entry_score   -- 删
    for each row
    begin
        DECLARE $module_id varchar(5);
        DECLARE $school_year varchar(15);
        DECLARE $semester nvarchar(5);
        select module_id into $module_id from evaluation_entry where entry_id=old.entry_id; -- 是哪个模块的
        select entry_school_year into $school_year from evaluation_entry where entry_id=old.entry_id; -- 是哪个学年
        select entry_semester into $semester from evaluation_entry where entry_id=old.entry_id; -- 是哪个学期   
        update module_score set score=score-old.score where student_id=old.student_id and module_id=$module_id
            and evaluation_school_year=$school_year and evaluation_semester=$semester;  -- 已存在，则更新
    end
//

-- 触发器，当添加班级时，同时计算测评年，并添加测评年班级链接表
create trigger tr_classes_insert after insert on classes
    for each row
    begin
        select concat(new.class_grade, '-', new.class_grade+1) into @school_year;
        insert lk_evaluation_year_classes
            values (1, new.class_id, new.class_small_id, @school_year, '秋');
        select concat(new.class_grade, '-', new.class_grade+1) into @school_year;
        insert lk_evaluation_year_classes
            values (2, new.class_id, new.class_small_id, @school_year, '春');
        select concat(new.class_grade+1, '-', new.class_grade+2) into @school_year;
        insert lk_evaluation_year_classes
            values (3, new.class_id, new.class_small_id, @school_year, '春');
        select concat(new.class_grade+2, '-', new.class_grade+3) into @school_year;
        insert lk_evaluation_year_classes
            values (4, new.class_id, new.class_small_id, @school_year, '春');
    end
//
create trigger tr_classes_update before update on classes
    for each row
    begin
        if new.class_grade!=old.class_grade
        then
            -- 先删除原来的连接表
            delete from lk_evaluation_year_classes where class_id=new.class_id and class_small_id=new.class_small_id;
            -- 再重新连接
            select concat(new.class_grade, '-', new.class_grade+1) into @school_year;
            insert lk_evaluation_year_classes
                values (1, new.class_id, new.class_small_id, @school_year, '秋');
            select concat(new.class_grade, '-', new.class_grade+1) into @school_year;
            insert lk_evaluation_year_classes
                values (2, new.class_id, new.class_small_id, @school_year, '春');
            select concat(new.class_grade+1, '-', new.class_grade+2) into @school_year;
            insert lk_evaluation_year_classes
                values (3, new.class_id, new.class_small_id, @school_year, '春');
            select concat(new.class_grade+2, '-', new.class_grade+3) into @school_year;
            insert lk_evaluation_year_classes
                values (4, new.class_id, new.class_small_id, @school_year, '春');
        end if;
    end
//

DELIMITER ;
    
-- 系统表
create table sysinfo
   (sysinfo_id 	int not null unique comment'为0有效',
    sysinfo_school_year		varchar(15) not null comment'当前学年, e.g.2011-2012', 
    sysinfo_semester   enum('秋', '春') not null comment'当前学期', 
    primary key(sysinfo_id)) comment'系统表';
    
-- 管理员表
create table administrater
   (admin_id 	varchar(15) not null unique comment'管理员ID',
    admin_pwd		varchar(40) not null comment'md5密码32位，明文密码6~15位',
    admin_descr   nvarchar(40) not null comment'身份描述，最多40字',
    primary key(admin_id)) comment'管理员表';
    
-- 政策表（各模块 优、良、合格、起评分 设定）
create table policy
   (module_id 	varchar(5) not null unique comment'模块ID，e.g. M1',
    policy_excellent		int comment'优，分',   -- 暂时可以为null
    policy_good		int comment'良，分',
    policy_pass		int comment'及格分',
    policy_basic  int comment'起评分',
    primary key(module_id),
    foreign key(module_id) references module(module_id) on delete cascade on update cascade) comment'政策表';
    
-- 公告表
create table news
   (news_id 	int	not null auto_increment comment'公告ID',
    news_title		nvarchar(40) not null comment'公告标题，最多40字',
    news_content   nvarchar(2000) not null comment'公告内容，最多2000字',
    new_date    datetime not null comment'公告发布时间',
    admin_id 	varchar(15) not null comment'公告发布者，管理员ID',
    primary key(news_id),
    foreign key(admin_id) references administrater(admin_id) on delete cascade on update cascade) comment'公告表';

/* populate relations */

/* 静态数据 */
insert into sysinfo values (0,	'2011-2012', '春');

insert into evaluation_year	values (1,	'大一第一学期');
insert into evaluation_year	values (2,	'大一第二学期');
insert into evaluation_year	values (3,	'大二全年');
insert into evaluation_year	values (4,	'大三全年');

insert into module values ('M1',	'思想道德素质模块');
insert into module values ('M2',	'专业理论素质模块');    -- M2也先加进去
insert into module values ('M3',	'创新精神与实践能力模块');
insert into module values ('M4',	'文化素质模块');
insert into module values ('M5',	'身心素质模块');

insert into policy values ('M1',	10, 5, 3, 7);
insert into policy values ('M2',	85,75,60, 0);
insert into policy values ('M3',	8, null, 4, 0);
insert into policy values ('M4',	10, null, 8, 6);
insert into policy values ('M5',	10, null, 8, 6);

/* 动态数据 */

insert into classes	values ('131011', '01', '2010');
insert into classes	values ('131011', '02', '2010');
insert into classes	values ('131012', '01', '2010');
insert into classes	values ('131012', '02', '2010');
insert into classes	values ('131013', '01', '2010');
insert into classes	values ('131013', '02', '2010');
insert into classes	values ('131014', '01', '2010');
insert into classes	values ('131014', '02', '2010');

insert into student values ('13101249','李寒', '男', '131013', '01');

-- 已写入触发器中 insert into lk_evaluation_year_classes values (1,	'131013', '01', '2010-2011', '秋');

insert into evaluation_entry values (1,	'2010-2011', '秋', '测试项目！项目简介！blabla。。END', '2010-10-01', 'M1');

insert into entry_score values ('13101249',	1, 3);

insert into administrater values ('tclh123',	'1234qwer', '管理员测试账户'); -- 测试时先用明文密码
insert into administrater values ('admin',	'1234qwer', 'sssta');
insert into administrater values ('weimeng',	'1234qwer', '卫萌老师');
insert into administrater values ('dy2010',	'1234qwer', '导员2010');
insert into administrater values ('dy2011',	'1234qwer', '导员2011');
insert into administrater values ('dy2012',	'1234qwer', '导员2012');
insert into administrater values ('dy2009',	'1234qwer', '导员2009');

insert into news values (1, '测试公告1', '测试公告内容！blabla...END!', '2012-07-15 20:14', 'tclh123');

