var generalCode = {
  "SYS_VEHICLE_NATURE":[
    {
        "code": "0",
        "name": "挂靠车"
    },
    {
        "code": "1",
        "name": "自购车"
    }
  ],
  "SYS_ANNUAL_AUDIT_STATUS":[
    {
        "code": "1",
        "name": "年审期内"
    },
    {
        "code": "2",
        "name": "年审过期"
    },
    {
        "code": "3",
        "name": "年审提醒"
    }
  ],
  "SYS_ACCIDENT_DUTY_TYPE":[
    {
        "code": "1",
        "name": "全部责任"
    },
    {
        "code": "2",
        "name": "主要责任"
    },
    {
        "code": "3",
        "name": "同等责任"
    },
    {
        "code": "4",
        "name": "次要责任"
    }
  ],
  "SYS_ACCIDENT_PROCESS_MODE":[
    {
        "code": "1",
        "name": "自行协商"
    },
    {
        "code": "2",
        "name": "快赔程序"
    },
    {
        "code": "3",
        "name": "现场调解"
    },
    {
        "code": "4",
        "name": "简易程序"
    },
    {
        "code": "5",
        "name": "一般处理"
    },
    {
        "code": "6",
        "name": "重大事故"
    }
  ],
  "SYS_ACCIDENT_SHAPE":[
    {
        "code": "1",
        "name": "正面碰撞"
    },
    {
        "code": "2",
        "name": "侧面碰撞"
    },
    {
        "code": "3",
        "name": "后面碰撞"
    }
  ],
  "SYS_VEHICLE_LOCK_STATUS":[
    {
        "code": "0",
        "name": "未锁车"
    },
    {
        "code": "1",
        "name": "已锁车"
    },
    {
        "code": "2",
        "name": "待锁车"
    },
    {
        "code": "3",
        "name": "锁车装置异常或被拆除"
    }
  ],
  "SYS_VEHICLE_TERMINAL_STATUS":[
     {
         "code": "-2",
         "name": "未绑定终端"
     },
     {
         "code": "-1",
         "name": "等待回应"
     },
     {
         "code": "0",
         "name": "获取成功"
     },
     {
         "code": "1",
         "name": "设备返回失败"
     },
     {
         "code": "2",
         "name": "发送失败"
     },
     {
         "code": "3",
         "name": "设备不支持"
     },
     {
         "code": "4",
         "name": "设备不在线"
     },
     {
         "code": "9",
         "name": "获取超时"
     }
   ],
  "SYS_VEHICLE_INSTRUCT_SENDED_STATUS":[
     {
         "code": "-1",
         "name": "等待"
     },
     {
         "code": "0",
         "name": "正常"
     },
     {
         "code": "1",
         "name": "设备返回失败"
     },
     {
         "code": "2",
         "name": "发送失败"
     },
     {
         "code": "3",
         "name": "设备不支持此功能"
     },
     {
         "code": "4",
         "name": "设备不在线"
     },
     {
         "code": "5",
         "name": "超时"
     },
     {
         "code": "6",
         "name": "返回数据异常"
     }
   ],
  "SYS_ROAD_LEVEL":[
     {
         "code": "1",
         "name": "村道"
     },
     {
         "code": "2",
         "name": "乡道"
     },
     {
         "code": "3",
         "name": "县道"
     },
     {
         "code": "4",
         "name": "省道"
     },
     {
         "code": "5",
         "name": "国道"
     }
   ],
  "SYS_OPERATION_MODE":[
     {
         "code": "1",
         "name": "定线运营"
     },
     {
         "code": "2",
         "name": "区域经营"
     },
     {
         "code": "3",
         "name": "循环运营"
     }
   ],
  "SYS_LINE_STATUS":[
     {
         "code": "1",
         "name": "营运"
     },
     {
         "code": "2",
         "name": "停运"
     },
     {
         "code": "3",
         "name": "终止"
     },
     {
         "code": "4",
         "name": "注销"
     },
     {
         "code": "5",
         "name": "其它"
     }
   ],
  "SYS_LINE_TYPE_ID":[
     {
         "code": "1",
         "name": "一类客运班线"
     },
     {
         "code": "2",
         "name": "二类客运班线"
     },
     {
         "code": "3",
         "name": "三类客运班线"
     },
     {
         "code": "4",
         "name": "四类客运班线"
     }
   ],
  "SYS_STATION_TYPE":[
     {
         "code": "1",
         "name": "起点"
     },
     {
         "code": "2",
         "name": "终点"
     },
     {
         "code": "3",
         "name": "途径点"
     }
   ],
  "SYS_REGIONALISM":[
    {
         "code":"110000",
         "name": "北京"
     },
    {
         "code":"120000",
         "name": "天津"
     },
    {
         "code":"130000",
         "name": "河北"
     },
    {
         "code":"410000",
         "name": "河南"
     },
    {
         "code":"430000",
         "name": "湖南"
     },
    {
         "code":"450000",
         "name": "广西"
     },
    {
         "code":"460000",
         "name": "海南"
     },
    {
         "code":"520000",
         "name": "贵州"
     },
    {
         "code":"610000",
         "name": "陕西"
     },
    {
         "code":"620000",
         "name": "甘肃"
     },
    {
         "code":"630000",
         "name": "青海"
     },
    {
         "code":"650000",
         "name": "新疆"
     }
   ],
  "SYS_INFO_TYPE_CLIENT":[
  /*{
         "code":"001",
         "name": "系统公告"
     },*/
    {
         "code":"002",
         "name": "企业公告"
     },
    /*{
         "code":"003",
         "name": "政策法规"
     },
    {
         "code":"004",
         "name": "行业快讯"
     },*/
    {
         "code":"005",
         "name": "企业资讯"
     }
   ]
};