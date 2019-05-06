module.exports = {
    menu: {
        'home': '首页',
        'activity': '近期活动',
        'declare': '申报活动',
        'language': '选择语言',
        'myActivity': '我的活动',
        'join': '参加的活动',
        'manage': '管理的活动',
        'check':'审核的活动',
        'loginOut': '退出',
        'login': '登录',
        'registered': '注册',
        'promptMsg': '请先登录醍摩豆账号',
        'rootManage': '后台管理'
    },
    home: {
        'centerTopTitle': '支持系统',
        'centerTopEn': 'SUPPORT SYSTEM',
        'hitaTitle': 'HITA智慧教室',
        'hitaInfo': '教师手机结合互动白板，开创智慧教学新风貌。',
        'tblTitle': 'TBL智慧教室',
        'tblInfo': '团队合作学习，学生自学、讨论、发表的环境。',
        'irsTitle': 'IRS智慧教室',
        'irsInfo': '生动、互动、主动的课堂，看见每个孩子的思考。',
        'mobileTitle': '移动学习智慧教室',
        'mobileInfo': '智能手机就是最好的课堂学习载具',
        'preTitle': '旗舰版智慧教室',
        'preInfo': '整合AI人工智能与多元教学型态于一体，智慧教室的极致展现',
        'proTitle': '电子书包智慧教室',
        'proInfo':'一生一平板促进个人化学习，训练学生自主探究培养高阶思维',
        'centerTitle': '近期活动',
        'centerEn': 'RECENT ACTIVITIES',
        'eventsUnit': '主办单位',
        'eventsInfo': '活动简介',
        'centerButtomTitle': '活动流程',
        'centerButtomEn': 'ACTIVITY PROCESS',
        'declare': '活动申报',
        'check': '活动审核',
        'signUp': '活动报名',
        'upload': '上传作品',
        'review': '评审作品',
        'announce': '公布成绩',
        'carouselTitle': '精彩记录',
        'carouselEn': 'PICTURE SHOW',
        'register': '立即报名',
        'eventEnd':'活动已结束'


    },
    footer: {
        'title':'醍摩豆智慧云',
        'copyright':'Copyright © 网奕资讯科技集团 | 醍摩豆（成都）信息技术有限公司版权所有'
    },
    declare: {
        basicTitle: '活动基本信息',
        activityName: '活动主题名称:',
        activityNameP: '请输入活动主题名称',
        introduce: '活动简介：',
        introduceP: '请输入活动主题简介',
        unit: '活动主办单位：',
        unitP: '请输入活动主办单位',
        file: '活动文件：',
        fileP: '上传活动文件',
        img: '主题图片：',
        imgP: '上传主题图片（1920*800px）',
        level: '活动类别：',
        levelItem: [
            //{
            //    id:1,
            //    label: '校级'
            //},
            //{
            //    id: 2,
            //    label: '区级'
            //},
            //{
            //    id: 3,
            //    label: '全国'
            //},
            //{
            //    id: 4,
            //    label: '国际'
            //}
            '校级', '区级', '全国', '国际'
        ],
        modular:'所需模块：',
        modulars: [
            {
                id: 1,
                label: "活动报名模块",
                mod: "signUp"
            },
            {
                id: 2,
                label: "上传作品模块",
                mod: "upload"
            },
            {
                id: 3,
                label: "作品评审模块",
                mod: "review"
            },
            {
                id: 4,
                label: "成绩公示模块",
                mod: "publish"
            }
        ],
        signTitle: '报名模块配置',
        signStart: '开始报名时间：',
        signEnd: '截至报名时间：',
        limitNum: '限制报名人数：',
        publishActivity: '公开活动信息：',
        publishName: '公布报名名单：',
        needHiTeach: '分配HiTeach序列号：',
        needIES: '分配IES账号：',
        needSocrates: '开通苏格拉底权限：',
        contestType: '参赛方式：',
        contestTypeItem: [
            {
                id: 1,
                label: '个人赛'
            },
            {
                id: 2,
                label: '团队赛'
            },
            {
                id: 3,
                label: '个人赛和团队赛'
            }
        ],
        groupNum:'团队人数：',
        uploadTitle: '上传作品配置',
        uploadStart: '开始上传时间：',
        uploadEnd: '截止上传时间：',
        uploadType: '上传作品类型：',
        uploadTypeItem: [
            { 
                id: 1,
                label: "文件",
            },
            {
                id: 2,
                label: "视频",
            },
            {
                id: 3,
                label: "作品链接",
            },
            {
                id: 4,
                label: "苏格拉底影片",
            }
        ],
        reviewTitle: '评审模块配置',
        reviewStart: '开始评审时间：',
        reviewEnd: '结束评审时间：',
        publishTitle: '成绩公示模块配置',
        publishStart: '开始公布时间：',
        publishEnd: '结束公布时间：',
        publishMode: '成绩公布方式：',
        publishModeType: [
            {
                id: 1,
                label:'上传获奖名单'
            },
            {
                id: 2,
                label:'自定义页面'
            }
        ],
        yesOrNo: [
            {
                id: 1,
                label:"是"
            },
            {
                id: 2,
                label:"否"
            }
        ],
        bookInfo: '报名填写信息：',
        start: "开始时间",
        end: "结束时间",
        submit: '提交',
        contacts: '活动负责人',
        contactsPhone: '负责人手机',
        contactsP: '请输入活动负责人',
        contactsPhoneP: '请输入负责人手机号码'
    },
    join: {
        joinState1: "所有的活动",
        joinState2: "已结束的活动",
        joinState3: "进行中的活动",
        joinTitle1: "活动基本信息",
        joinTitle2: "报名信息",
        joinTitle3: "作品信息",
        joinTitle4: "成绩公布信息",
        joinData: "暂无数据",
        joinLang: "请选择参加的活动",
        joinManual: "点击查看报名操作手册",
        joinIES: "上传参赛作品",
        joinTeam: "组队信息",
        joinOut: "退出团队",
        joinNull: "暂无组队信息，前往加入团队",
        joinInTeam: "加入团队",
        joinIn: "加入",
        joinCreate: '创建',
        overNum:'此团队人数已满，请加入其它团队，或联系此团队人员！'
    },
    selectActivity:{
        msg: '查看管理的活动',
        prompt:'暂无匹配数据'
    },
    signUp: {
        teamPrompt: "组队教师必须是同一所学校，且分别报名不同的学科！",
        signLabel: '填写报名信息',
        fixLabel: '修改报名信息',
        confirm: "确认",
        confirmInfo: '修改信息成功',
        submit: '立刻报名',
        member: '团队成员：',
        teamName:'团队名称：',
        teamNameP: '请输入团队名称',
        teamJoin: "加入",
        teamInput: "加入团队",
        teamCreat:"创建团队"
    },
    ConfirmeDecInfo: {
        download: "下载文件",
        cancel: "取消",
        confirm:"确认"
    },
    manageSport: {
        manageAct: "管理的活动",
        manageFind: "查看活动",
        manageStatu: "管理活动状态",
        manageReview: "查看报名数据",
        manageUpload: "查看上传数据",
        manageInfo: "导入评审信息",
        managePoint: "分配评审作品",
        manageProgress: "查看评审进度",
        manageNum: "查看作品分数",
        manageView: "活动数据图表",
        manageStart: "待启动的活动",
        manageIn: "进行中的活动",
        manageEnd: "已结束的活动",
        manageAdd: '添加管理员'
    },
    manageInfo: {
        manageTitle: "活动基本信息",
        titleName: "活动主题名称",
        manageIntroduce: "活动简介",
        manageUnit: "活动主办单位",
        manageFile: "活动文件",
        manageUpload: "点击下载查看文件",
        manageLevel: "活动范围",
        manageStatus: "活动状态",
        manageId: "申报人",
        manageTeamModelId: "申报者醍摩豆ID",
        ManageinfoData:"暂无活动数据"
    },
    basicInfo: {
        basicUnit: "主办单位：",
        basicLevel: "活动范围：",
        basicStartDate: "开始时间：",
        basicEndDate: "截止时间：",
        basicMatch:"大赛文件：",
        basicFile: "下载文件：",
        basicCompetition: "大赛简介：",
        basicIntroduce: "下载活动文件", 
        
    },
    checkSport: {
        checkAct: "审核的活动",
        checkFind: "查看活动",
        checkData: "查看报名数据",
        checkUpload: "查看上传数据",
        checkInput: "导入评审信息",
        checkReview: "分配评审作品",
        checkSpeed: "查看评审进度",
        checkNum: "查看作品分数",
        checkAudited: "已审核的活动",
        checkUnaudited: "未审核的活动",
        checkPass: "审核通过的活动"
    },
    checkInfo: {
        infoBasic: "活动基本信息",
        infoName: "活动主题名称",
        infoIntroduce: "活动简介",
        infoUnit: "活动主办单位",
        infoFile: "活动文件",
        infoFileUrl: "点击下载查看文件",
        infoLevel: "活动级别",
        infoStatus: "活动状态",
        infoDeclarant: "申报人",
        infoTeamModelId: "申报者醍摩豆ID",
        infoData: "暂无活动数据"
    },
    rulePrompt: {
        name: '请输入活动主题名称',
        introduce:  '请输入主题简介',
        level: '请选择活动类别',
        unit: '请输入主办单位',
        fileUrl: '请上传活动相关文件',
        imgUrl: '请上传活动主题图片',
        limitNum: '请选择是否限制报名人数',
        publishActivity: '请选择是否首页显示活动信息',
        entryCard:  '请选择是否生成参赛证明',
        publishName: '请选择是否公布报名成功名单',
        contestType:  '请选择参赛方式',
        needHiTeach:  '请选择是否分配HiTeach序列号',
        needIES: '请选择是否分配IES账号',
        needSocrates: '请选择是否开通苏格拉底权限',
        partIds: '请选择活动报名需要填写的信息',
        uploadType: '请选择上传作品类型',
        publishMode: '请选择公布成绩方式',
        contactsPhone:'请输入负责人手机号',
        contacts: '请输入负责人姓名',
    },
    formConfigP: {
        input: '请输入',
        select: '请选择',
        country: '国家',
        province: '省份',
        city: '城市',
        school: '学校'
    },
    prompt: {
        signUp: {
            completeInfo: '请完善报名信息，再报名！',
            loginOut: '登录失效，请重新登录进行报名！',
            teamNumFirst: '团队人数不能超过',
            teamNumLast: '人！(包括自己)',
            teamRuleFirst: '竞赛规定团队人数为',
            teamRuleLast: '人，请继续添加组员。如果组员还未报名请最后报名的组员进行组队！',
            success:'您已报名成功，可在我参加的活动中查看活动信息！'
        },
        homePage: {
            activityStatus: '此活动已结束！'
        },
        homeHeader: {
            login: '请登录醍摩豆账号！',
            manage:'申报活动请联系管理员！'
        }
    },
    public: {
        year: '年',
        month: '月',
        day:'日'
    }

}