<style lang="less" scoped>
  @import './smart-class-dashboard.less';
</style>

<template>
    <div id="smart-dashboard">
      <div class="Header">
        <Row type="flex" justify="center" align="middle">
          <Col :span="3">
            <img style="height: 20px;" src="../../../assets/mark.svg"/>
          </Col>
          <Col :span="8">
            <Icon size="20" type="md-pin" style="vertical-align: middle;color: #94998a" /><span class="smart-markD">{{ $t('breadcrumbs')}}</span>
          </Col>
          <Col :span="11">
            <Menu class="menu" mode="horizontal" theme="dark">
              <MenuItem name="1">
                  <Icon type="ios-paper" />
                  {{ $t('menu1')}}
              </MenuItem>
              <MenuItem name="2">
                  <Icon type="ios-people" />
                  {{ $t('menu2')}}
              </MenuItem>
              <MenuItem name="3">
                  <Icon type="ios-people" />
                  {{ $t('menu3')}}
              </MenuItem>
              <MenuItem name="4">
                  <Icon type="ios-construct" />
                  {{ $t('menu4')}}
              </MenuItem>
          </Menu>
          </Col>
          <Col :span="2" style="text-align: center;">
            <!-- <Icon style="font-size: 24px; color: #FAFAFA;" type="md-person" /> -->
            <img style="width: 35px;" src="@/assets/image/touxiang.png">
          </Col>
        </Row>
      </div>
      <div class="content">
        <div style="text-align: center;padding-top: 5px;height: 5%;">
          <Button to="/smartiotdashboard" class="dashBoardBtn">{{$t('smartClassDB.pageButton2')}}</Button>
          <Button class="dashBoardBtn select">{{$t('smartClassDB.pageButton1')}}</Button>
        </div>
        <Row class="box" type="flex" justify="center">
          <Col style="padding: 0 7px"  :span="4"  class="block-1">
            <div style="height: 100%;">
              <div class="block-border" style="height: 19%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                <!-- 边角设计 start-->
                <div class="image-border image-border-left-top" ></div>
                <div class="image-border image-border-right-top" ></div>
                <div class="image-border image-border-left-bottom" ></div>
                <div class="image-border image-border-lright-bottom" ></div>
                <!-- 边角设计 end-->
                <Row class="total" type="flex" align="middle" justify="space-around">
                  <Col :span="10">
                    <div class="total-box">
                      <div class="total-box-border total-box-left-top" ></div>
                      <div class="total-box-border total-box-right-top" ></div>
                      <div class="total-box-border total-box-left-bottom" ></div>
                      <div class="total-box-border total-box-right-bottom" ></div>
                      <span class="total-box-title">{{$t('smartClassDB.block1SubTitle1')}}</span>
                      <h5 class="total-box-number">2,056</h5>
                      <span class="total-box-subtitle">{{$t('smartClassDB.block1SubTitle3')}}: <span class="total-box-subtitle-num">38</span></span>
                    </div>
                  </Col>
                  <Col :span="10">
                    <div class="total-box" style="height: 100%;text-align: center;">
                      <div class="total-box-border total-box-left-top" ></div>
                      <div class="total-box-border total-box-right-top" ></div>
                      <div class="total-box-border total-box-left-bottom" ></div>
                      <div class="total-box-border total-box-right-bottom" ></div>
                      <span class="total-box-title">{{$t('smartClassDB.block1SubTitle2')}}</span>
                      <h5 class="total-box-number">81,762</h5>
                      <span class="total-box-subtitle">{{$t('smartClassDB.block1SubTitle3')}}: <span class="total-box-subtitle-num">183</span></span>
                    </div>
                  </Col>
                </Row>
              </div>  
              <div style="height: 1.5%"></div>
              <div class="block-border" style="height: 30%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                <!-- 边角设计 start-->
                <div class="image-border image-border-left-top" ></div>
                <div class="image-border image-border-right-top" ></div>
                <div class="image-border image-border-left-bottom" ></div>
                <div class="image-border image-border-lright-bottom" ></div>
                <!-- 边角设计 end-->
                <div style="height: 20%;">
                  <h5 style="height: 100%;padding: 3% 4%;font-size: 1.25em;color: rgb(228, 233, 220);border-bottom: solid 1px #94998a;font-weight: 100;">{{$t('smartClassDB.block1Title1')}}</h5>
                </div>
                <div style="height: 70%;">
                  <AccountLine :id="'accountline1'"></AccountLine>
                </div>
              </div>  
              <div style="height: 1.5%"></div>
              <div class="block-border" style="height: 48%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                <!-- 边角设计 start-->
                <div class="image-border image-border-left-top" ></div>
                <div class="image-border image-border-right-top" ></div>
                <div class="image-border image-border-left-bottom" ></div>
                <div class="image-border image-border-lright-bottom" ></div>
                <!-- 边角设计 end-->
                <div style="height: 13%;">
                  <h5 style="height: 100%;padding: 3% 4%;font-size: 1.25em;color: #E4E9DC;border-bottom: solid 1px #94998a;font-weight: 100;">{{$t('smartClassDB.block1Title2')}}</h5>
                </div>
                <div style="height: 87%;width:100%;position: relative;overflow: auto;">
                  <div class="scrollstyle" style="position: absolute;right: 0px;left: 0;top: 0;bottom: 0;overflow-x: hidden;overflow-y: auto;;background-color: rgba(17, 17, 17, 0.14);">
                      <div class="course" v-for="(item, index) in courseList" :key="index">
                          <Row type="flex" justify="center" align="middle" class="course-Row">
                            <Col :span="16">
                              <h5 class="ellipsis course-className">{{ item.className }}</h5>
                              <span class="ellipsis course-user">{{item.user }}</span><Icon class="course-icon" :size="15" type="ios-information-circle" />
                            </Col>
                            <Col :span="8">
                              <div class="course-text" style="margin-bottom: 3px;">{{$t('smartClassDB.block1SubTitle4')}} | {{ item.courses }} {{$t('unit6')}}</div>
                              <div class="course-text">{{$t('smartClassDB.block1SubTitle5')}} | {{ item.students }} {{$t('unit7')}}</div>
                            </Col>
                          </Row>
                      </div>
                  </div>
                </div>
              </div>  
            </div>
          </Col>
          <Col  style="padding: 0 7px" :span="6" class="block-2">
            <!-- block-2 -->
            <div class="block-border" style="height: 38%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 77%">
                <ClassTypePie :id="'classtypepie'"></ClassTypePie>
              </div>
              <div style="height: 23%;padding-top: 10px;">
                 <Row type="flex" justify="space-around" align="middle">
                  <Col :span="6" style="padding: 0 10px;">
                    <div class="classroom">
                      <span class="classroom-title">{{$t('smartClassDB.block2SubTitle1')}}</span>
                      <h5 class="classroom-number">521</h5>
                    </div>
                  </Col>
                  <Col :span="6" style="padding: 0 10px;">
                    <div class="classroom">
                      <span class="classroom-title">{{$t('smartClassDB.block2SubTitle2')}}</span>
                      <h5 class="classroom-number">857</h5>
                    </div>  
                  </Col>
                  <Col :span="6" style="padding: 0 10px;">
                    <div class="classroom">
                      <span class="classroom-title">{{$t('smartClassDB.block2SubTitle3')}}</span>
                      <h5 class="classroom-number">410</h5>
                    </div>  
                  </Col>
                  <Col :span="6" style="padding: 0 10px;">
                    <div class="classroom">
                      <span class="classroom-title">{{$t('smartClassDB.block2SubTitle4')}}</span>
                      <h5 class="classroom-number">242</h5>
                    </div>  
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 25%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="today-class-content">
                <h5 class="today-class-content-title">{{$t('smartClassDB.block3Title1')}}</h5>
                <Row class="today-class-content-contents" type="flex" justify="center" align="middle">
                  <Col :span="10" class="chart">
                    <RingPie :id="'ringPie1'"></RingPie>
                  </Col>
                  <Col :span="14" class="detail">
                    <Row class="detail-row" type="flex" justify="center" align="middle">
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','苏格拉底')" @mouseenter="pieHeightLight('class','苏格拉底')">
                          <span class="detail-title detail-title-border-red">苏格拉底</span>
                          <h5 class="detail-amount">33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','电子笔记')" @mouseenter="pieHeightLight('class','电子笔记')">
                          <span  class="detail-title detail-title-border-pink" >电子笔记</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','上传影片')" @mouseenter="pieHeightLight('class','上传影片')">
                          <span  class="detail-title detail-title-border-blue" >上传影片</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','作业作品')" @mouseenter="pieHeightLight('class','作业作品')">
                          <span  class="detail-title detail-title-border-green">作业作品</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','翻转课堂')" @mouseenter="pieHeightLight('class','翻转课堂')">
                          <span  class="detail-title detail-title-border-yellow" >翻转课堂</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="8" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','线上测验')" @mouseenter="pieHeightLight('class','线上测验')">
                          <span  class="detail-title detail-title-border-orange" >线上测验</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 34%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="course-content">
                <div class="course-content-title">
                  <h5>{{$t('smartClassDB.block3Title2')}}</h5>
                </div>
                <Row class="course-content-contents" type="flex" justify="center" align="middle">
                  <Col class="course-content-contents-left" :span="10">
                    <Row class="course-content-contents-left-row" type="flex"  justify="center" align="middle">
                      <Col class="course-content-contents-left-row-col" :span="12">
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-red">模拟测验</span>
                          <h5 class="contents-box-num">0.0%</h5>
                          <span class="contents-box-subtitle">总数量: 0</span>
                        </div>
                      </Col>
                      <Col class="course-content-contents-left-row-col" :span="12">
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-green">成绩登录</span>
                          <h5 class="contents-box-num">0.5%</h5>
                          <span class="contents-box-subtitle">总数量: 28</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col">
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-pink">线上测验</span>
                          <h5 class="contents-box-num">1.5%</h5>
                          <span class="contents-box-subtitle">总数量:77</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col" >
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-yellow">合併活动</span>
                          <h5 class="contents-box-num">0.4%</h5>
                          <span class="contents-box-subtitle">总数量: 20</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col">
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-purple">班级竞赛</span>
                          <h5 class="contents-box-num">4.6%</h5>
                          <span class="contents-box-subtitle">总数量: 227</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col" >
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-orange">网路阅卷</span>
                          <h5 class="contents-box-num">4.1%</h5>
                          <span class="contents-box-subtitle">总数量: 214</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col" >
                        <div class="contents-box">
                          <span class="contents-box-title contents-box-border-blue">HiTeach</span>
                          <h5 class="contents-box-num">88.9%</h5>
                          <span class="contents-box-subtitle">总数量: 4531</span>
                        </div>
                      </Col>
                      <Col :span="12" class="course-content-contents-left-row-col" >
                      
                      </Col>
                    </Row>
                  </Col>
                  <Col :span="14" style="height: 100%;">
                    <div style="height:65%">
                      <CourseBar :id="'coursebar1'"></CourseBar>
                    </div>
                    <div style="height:35%">
                      <Row type="flex" justify="center" align="middle" >
                        <Col :span="11">
                          <RingPie :singleColor="true" :id="'ringPie3'"></RingPie>
                        </Col>
                        <Col :span="13">
                            <div style="color: #A9B2B8">{{$t('smartClassDB.block3SubTitle1')}} : <span style="color: #fafafa;">54</span></div>
                            <div style="color: #A9B2B8">{{$t('smartClassDB.block3SubTitle2')}} : <span style="color: #fafafa;">48</span></div>
                            <div style="color: #A9B2B8">{{$t('smartClassDB.block3SubTitle3')}} : <span style="color: #fafafa;">49</span></div>
                        </Col>
                      </Row>
                    </div>
                  </Col>
                </Row>
              </div>
            </div>
          </Col>
          <Col style="padding: 0 7px" :span="12">
            <Row style="height: 64.5%">
              <Col :span="8" class="block-3">
                <div class="block-border" style="height: 47.5%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                  <!-- 边角设计 start-->
                  <div class="image-border image-border-left-top" ></div>
                  <div class="image-border image-border-right-top" ></div>
                  <div class="image-border image-border-left-bottom" ></div>
                  <div class="image-border image-border-lright-bottom" ></div>
                  <!-- 边角设计 end-->
                  <div class="today-class-content">
                    <h5 class="today-class-content-title">{{$t('smartClassDB.block3Title3')}}</h5>
                    <Row type="flex" justify="center" align="middle" class="today-class-content-contents">
                      <Col :span="13" class="chart">
                        <RingPie  style="height: 60%" :tooltip="true" :pieData="[{value:3719, name:'题目数'}, {value:0, name:'分享课例数'},{value:251, name:'教材数'}, {value:0, name:'校本课纲数'}]" :id="'ringPie2'"></RingPie>
                        <div style="height: 30%;text-align: center;" >
                          <span style="color: #94998a;">{{$t('smartClassDB.block3SubTitle4')}}</span>
                          <h5 style="font-size: 2em;color: #fafafa;font-weight: 100;">7,689</h5>
                        </div>
                      </Col>
                      <Col :span="11" class="detail">
                        <Row class="detail-row" >
                          <Col  class="detail-col">
                            <div @mouseleave="pieDownPlay('class','题目数')" @mouseenter="pieHeightLight('class','题目数')">
                              <span class="detail-title detail-title-border-red">题目数</span>
                              <h5 class="detail-amount">3,719 <small>(48.36%)</small></small></h5>
                            </div>
                          </Col>
                          <Col  class="detail-col">
                            <div @mouseleave="pieDownPlay('class','教材数')" @mouseenter="pieHeightLight('class','教材数')">
                              <span  class="detail-title detail-title-border-blue" >教材数</span>
                              <h5 class="detail-amount" >251 <small>(3.26%)</small></h5>
                            </div>
                          </Col>
                          <Col  class="detail-col">
                            <div @mouseleave="pieDownPlay('class','分享课例数')" @mouseenter="pieHeightLight('class','分享课例数')">
                              <span  class="detail-title detail-title-border-pink" >分享课例数</span>
                              <h5 class="detail-amount" >3,104 <small>(0%)</small></h5>
                            </div>
                          </Col>
                          <Col  class="detail-col">
                            <div @mouseleave="pieDownPlay('class','校本课纲数')" @mouseenter="pieHeightLight('class','校本课纲数')">
                              <span  class="detail-title detail-title-border-yellow" >校本课纲数</span>
                              <h5 class="detail-amount" >615 <small>(0%)</small></h5>
                            </div>
                          </Col>
                        </Row>
                      </Col>
                    </Row>
                  </div>             
                </div>
                <div style="height: 1.5%"></div>
                <div class="block-border" style="height: 51%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                  <!-- 边角设计 start-->
                  <div class="image-border image-border-left-top" ></div>
                  <div class="image-border image-border-right-top" ></div>
                  <div class="image-border image-border-left-bottom" ></div>
                  <div class="image-border image-border-lright-bottom" ></div>
                  <!-- 边角设计 end-->   
                  <h5 style="border-bottom: 1px solid #94998a;font-size: 1.25em;color: #E4E9DC;font-weight: 100;height: 15%;padding: 10px;">各年級資源產出分佈</h5>
                  <div style="height: 85%;">
                    <ClassResourceBar :id="'classresourcebar1'"></ClassResourceBar>
                  </div>
                </div>
              </Col>
              <Col style="padding-left: 14px;" :span="16" class="block-4">
                <div class="block-border" style="height: 31.5%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                  <!-- 边角设计 start-->
                  <div class="image-border image-border-left-top" ></div>
                  <div class="image-border image-border-right-top" ></div>
                  <div class="image-border image-border-left-bottom" ></div>
                  <div class="image-border image-border-lright-bottom" ></div>
                  <!-- 边角设计 end-->
                  <div style="height: 100%">
                    <div style="height: 20%;">
                      <h5 style="font-size: 1.25em;color: #E4E9DC;padding: 10px 0 15px 15px;font-weight: 100;">{{$t('smartClassDB.block4Title1')}}</h5>
                    </div>
                    <div class="teachingEvent">
                      <Row type="flex" justify="center" align="middle" class="teachingEvent-row">
                        <Col class="teachingEvent-row-col" :span="17">
                          <Row type="flex" justify="space-around" align="middle" class="teachContent">
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle1')}}</span>
                              <span class="teachContent-box-num">175</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle2')}}</span>
                              <span class="teachContent-box-num">537</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle3')}}</span>
                              <span class="teachContent-box-num">643</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>                
                          </Row>
                        </Col>
                        <Col :span="7" style="height:100%">
                          <RingPie :id="'ringPie4'"></RingPie>
                        </Col>
                      </Row>
                    </div>
                  </div>
                </div>
                <div style="height: 2.3%"></div>
                <div class="block-border" style="height: 31.5%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                  <!-- 边角设计 start-->
                  <div class="image-border image-border-left-top" ></div>
                  <div class="image-border image-border-right-top" ></div>
                  <div class="image-border image-border-left-bottom" ></div>
                  <div class="image-border image-border-lright-bottom" ></div>
                  <!-- 边角设计 end-->
                  <div style="height: 100%">
                    <div style="height: 20%;">
                      <h5 style="font-size: 1.25em;color: #E4E9DC;padding: 10px 0 15px 15px;font-weight: 100;">{{$t('smartClassDB.block4Title2')}}</h5>
                    </div>
                    <div class="teachingEvent">
                      <Row type="flex" justify="center" align="middle" class="teachingEvent-row">
                        <Col class="teachingEvent-row-col" :span="17">
                          <Row type="flex" justify="space-around" align="middle" class="teachContent">
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle5')}}</span>
                              <span class="teachContent-box-num">519</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle6')}}</span>
                              <span class="teachContent-box-num">410</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>
                            <Col class="teachContent-box" :span="6">
                              <span class="teachContent-box-title">{{$t('smartClassDB.block4SubTitle7')}}</span>
                              <span class="teachContent-box-num">426</span>
                              <span class="teachContent-box-subnum">{{$t('smartClassDB.block4SubTitle4')}} : 11</span>
                            </Col>                
                          </Row>
                        </Col>
                        <Col :span="7" style="height:100%">
                          <RingPie :id="'ringPie5'"></RingPie>
                        </Col>
                      </Row>
                    </div>
                  </div>
                </div>
                <div style="height: 2.3%"></div>
                <div style="height: 32.5%;">
                  <Row style="height: 100%;">
                    <Col :span="8" style="height: 100%;padding-right:1.5%;">
                      <div class="block-border" style="height: 100%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                        <!-- 边角设计 start-->
                        <div class="image-border image-border-left-top" ></div>
                        <div class="image-border image-border-right-top" ></div>
                        <div class="image-border image-border-left-bottom" ></div>
                        <div class="image-border image-border-lright-bottom" ></div>
                        <!-- 边角设计 end-->
                        <h5 style="height: 15%;font-size: 1.25em;color: #E4E9DC;padding-top: 10px;font-weight: 100;text-align: center;">{{$t('smartClassDB.block4Title3')}}</h5>
                        <div style="height: 85%">
                          <FinishPercentPie :id="'finishpercentpie1'"></FinishPercentPie>
                        </div>
                      </div>
                    </Col>
                    <Col :span="8" style="height: 100%;padding:0 1%;">
                      <div class="block-border" style="height: 100%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                        <!-- 边角设计 start-->
                        <div class="image-border image-border-left-top" ></div>
                        <div class="image-border image-border-right-top" ></div>
                        <div class="image-border image-border-left-bottom" ></div>
                        <div class="image-border image-border-lright-bottom" ></div>
                        <!-- 边角设计 end-->
                        <h5 style="height: 15%;font-size: 1.25em;color: #E4E9DC;padding-top: 10px;font-weight: 100;text-align: center;">{{$t('smartClassDB.block4Title4')}}</h5>
                        <div style="height: 85%">
                          <FinishPercentPie :pieData="{total: 537, value: 354}" :id="'finishpercentpie2'"></FinishPercentPie>
                        </div>                        
                      </div>
                    </Col>
                    <Col  :span="8" style="height: 100%;padding-left: 1.5%;">
                      <div class="block-border"  style="height: 100%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                        <!-- 边角设计 start-->
                        <div class="image-border image-border-left-top" ></div>
                        <div class="image-border image-border-right-top" ></div>
                        <div class="image-border image-border-left-bottom" ></div>
                        <div class="image-border image-border-lright-bottom" ></div>
                        <!-- 边角设计 end-->
                        <h5 style="height: 15%;font-size: 1.25em;color: #E4E9DC;padding-top: 10px;font-weight: 100;text-align: center;">{{$t('smartClassDB.block4Title5')}}</h5>
                        <div style="height: 85%">
                          <FinishPercentPie :pieData="{total: 643, value:379}" :id="'finishpercentpie3'"></FinishPercentPie>
                        </div>
                      </div>
                    </Col>
                  </Row>
                </div>
              </Col>
            </Row>
            <div style="height: 1.5%"></div>
            <Row style="height: 34%;">
              <Col style="height: 100%;"> 
                <div class="block-border" style="height: 100%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                  <!-- 边角设计 start-->
                  <div class="image-border image-border-left-top" ></div>
                  <div class="image-border image-border-right-top" ></div>
                  <div class="image-border image-border-left-bottom" ></div>
                  <div class="image-border image-border-lright-bottom" ></div>
                  <!-- 边角设计 end-->
                  <div style="height: 20%">
                    <h5 style="font-size: 1.25em;color: #E4E9DC;padding: 15px 0 0 15px;font-weight: 100;">{{$t('smartClassDB.block4Title6')}}</h5>                
                  </div>
                  <div style="height: 80%">
                    <TotalLine :id="'totalLine1'"></TotalLine>
                  </div>
                </div>  
              </Col>
            </Row>
          </Col>
        </Row>
      </div>
    </div>
</template>

<script>
import AccountLine from '@/components/graph/accountLine'
import TeachScienceBar from '@/components/graph/teachScienceBar'
import ClassTypePie from '@/components/graph/classtypePie'
import RingPie from '@/components/graph/ringPie'
import CourseBar from '@/components/graph/courseBar'
import FinishPercentPie from '@/components/graph/finishPercentPie'
import TotalLine from '@/components/graph/totalLine'
import ClassResourceBar from '@/components/graph/classResourceBar'

export default {
  name:'smart-dashboard',
  data () {
    return {
      courseList:[
        {className: '六年级数学', user: '布鲁斯', students: 42, courses: 62},
        {className: '一年级物理', user: 'Picc', students: 30, courses: 51},
        {className: '三年级英文', user: '薇拉', students: 25, courses: 45},
        {className: '三年级语文', user: '梁老师', students: 42, courses: 27},
        {className: '六年级化学', user: '贾斯汀', students: 32, courses: 14},
        {className: '七年级理化', user: '陈老师', students: 33, courses: 34},
        {className: '八年级地球科学', user: '彭老师', students: 25, courses: 52},
        {className: '六年级历史', user: '王老师', students: 37, courses: 32},
        {className: '九年级健康教育', user: '里欧', students: 42, courses: 52},
        {className: '二年级童军', user: 'Daphne', students: 30, courses: 30},
      ]     
    }
  },
  components:{
    AccountLine,
    TeachScienceBar,
    ClassTypePie,
    RingPie,
    CourseBar,
    FinishPercentPie,
    TotalLine,
    ClassResourceBar
  },
  methods:{   
  }
}
</script>
