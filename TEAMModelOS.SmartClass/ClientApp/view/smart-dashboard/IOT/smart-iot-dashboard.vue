<style lang="less" scoped>
  @import './smart-iot-dashboard.less';
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
            <img style="width: 35px;" src="@/assets/image/touxiang.png">
          </Col>
        </Row>
      </div>
      <div class="content">
        <div style="text-align: center;padding-top: 5px;height: 5%;">
          <Button class="dashBoardBtn select">{{ $t('smartIOTDB.pageButton1') }}</Button>
          <Button to="/smartclassdashboard" class="dashBoardBtn">{{ $t('smartIOTDB.pageButton2') }}</Button>
        </div>
        <Row class="box" type="flex" justify="center">
          <Col  :span="8"  class="block-1 block-border">
            <!-- block-1 -->
            <!-- 边角设计 start-->
            <div class="image-border image-border-left-top" ></div>
            <div class="image-border image-border-right-top" ></div>
            <div class="image-border image-border-left-bottom" ></div>
            <div class="image-border image-border-lright-bottom" ></div>
            <!-- 边角设计 end-->
            <div style="height: 100%;">
              <div style="height: 60%;width:100%;">
                <div style="height: 100%;">
                  <div style="height: 80%;text-align: center;">
                    <img id="planImg" style="width: 100%;height: 100%;display: none;" src="@/assets/image/floorplan.png" />
                    <img id="pin" style="display: none;" src="@/assets/pin.svg" />
                    <img id="greenpin" style="display: none;" src="@/assets/greenpin.svg" />
                    <img id="redpin" style="display: none;" src="@/assets/redpin.svg" />
                    <canvas id="floorplan" style="width: 100%;height: 100%;">
                      当前浏览器不支持canvas
                      <!-- 如果浏览器支持canvas，则canvas标签里的内容不会显示出来 -->
                    </canvas>
                  </div>
                  <div style="height: 20%;padding: 0 5%;border-bottom: 1px solid #94998a;">
                    <Row>
                      <Col :span="5" style="text-align: center;">
                        <span style="color: #94998a;">{{$t('smartIOTDB.block1Title1')}}</span>
                        <h5 style="font-size: 2em;color: #FAFAFA;font-weight: 100;">4 <small>(36%)</small></h5>
                      </Col>
                      <Col :span="5" style="text-align: center;">
                      <span style="color: #94998a;">{{$t('smartIOTDB.block1Title2')}}</span>
                        <h5 style="font-size: 2em;color: #FAFAFA;font-weight: 100;">1 <small>(9%)</small></h5>
                      </Col>
                      <Col :span="5" style="text-align: center;">
                      <span style="color: #94998a;">{{$t('smartIOTDB.block1Title3')}}</span>
                        <h5 style="font-size: 2em;color: #FAFAFA;font-weight: 100;">3 <small>(27%)</small></h5>
                      </Col>
                      <Col :span="5" style="text-align: center;">
                      <span style="color: #94998a;">{{$t('smartIOTDB.block1Title4')}}</span>
                        <h5 style="font-size: 2em;color: #FAFAFA;font-weight: 100;">18 <small>(77%)</small></h5>
                      </Col>
                      <Col :span="4"  style="text-align: center;">
                        <span style="color: #94998a;">{{$t('smartIOTDB.block1Title5')}}</span>
                        <h5 style="font-size: 2em;color: #FAFAFA;font-weight: 100;">11</h5>
                      </Col>
                    </Row>
                  </div>
                </div>
              </div>
              <div style="height: 40%;width:100%;position: relative;overflow: auto;">
                <div class="scrollstyle" style="position: absolute;right: 0px;left: 0;top: 0;bottom: 0;overflow-x: hidden;overflow-y: auto;">
                  <div class="classInfo" v-for="(item, index) in classInfoData" :key="index">
                    <Row type="flex" justify="center" align="bottom" style="border-bottom: solid 1px #94998a;padding-bottom: 7px;">
                      <Col :span="15">
                        <h5>{{ item.className }}</h5>
                        <span class="ellipsis subtitle">{{ item.classProduct }}&nbsp;&nbsp;|&nbsp;&nbsp;{{ item.classDetail }}</span>
                      </Col>
                      <Col :span="9">
                        <span class="ellipsis teacherName">{{ item.teacherName}}</span>&nbsp;<Icon class="teachIcon" :size="15" type="ios-information-circle" />
                        <Row  style="display: block;">
                          <Col :span="12" style="color: #48DBFC;">
                            {{$t('smartIOTDB.hotclass')}}&nbsp;&nbsp;|&nbsp;&nbsp;{{item.classHot + $t('unit1')}}
                          </Col>
                          <Col :span="12" :class="classModeClass(item.classMode)">
                            {{ classModeStr(item.classMode) }}&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;{{item.classTime}}
                          </Col>
                        </Row>
                      </Col>
                    </Row>
                  </div>
                </div>
              </div>
            </div>
          </Col>
          <Col  :span="5" class="block-2">
            <!-- block-2 -->
            <div class="block-border" style="height: 12%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <Row type="flex" justify="center" class="info-row">
                <Col :span="9" class="info-col-main">
                  <div class="info-col-content">
                    <span>{{ $t('smartIOTDB.block2Title1')}}</span><br/><small>{{ $t('smartIOTDB.block2SubTitle1')}}</small>
                    <h5>8<small>/9</small></h5>
                  </div>            
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle2')}}</small>
                    <h5>8</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle3')}}</small>
                    <h5>8</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle4')}}</small>
                    <h5>8</h5>
                  </div> 
                </Col>
              </Row>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 12%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <Row type="flex" justify="center" class="info-row">
                <Col :span="9" class="info-col-main">
                  <div class="info-col-content">
                    <span>{{ $t('smartIOTDB.block2Title2')}}</span><br/><small>{{ $t('smartIOTDB.block2SubTitle1')}}</small>
                    <h5>54<small>/65</small></h5>
                  </div>            
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle2')}}</small>
                    <h5>61.2</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle3')}}</small>
                    <h5>58.9</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle4')}}</small>
                    <h5>53.1</h5>
                  </div> 
                </Col>
              </Row>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 12%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <Row type="flex" justify="center" class="info-row">
                <Col :span="9" class="info-col-main">
                  <div class="info-col-content">
                    <span>{{ $t('smartIOTDB.block2Title3')}}</span><br/><small>{{ $t('smartIOTDB.block2SubTitle1')}}</small>
                    <h5>1,892<small>/2,863</small></h5>
                  </div>            
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle2')}}</small>
                    <h5>2,542</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content border-right">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle3')}}</small>
                    <h5>2,301</h5>
                  </div> 
                </Col>
                <Col :span="5"  class="info-col-secondary">
                  <div class="info-col-content">
                    <br/><small>{{ $t('smartIOTDB.block2SubTitle4')}}</small>
                    <h5>2,436</h5>
                  </div> 
                </Col>
              </Row>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 29%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height:20%;">
                <h5 style="font-size: 1.25em;color:#E4E9DC;padding: 15px 0 15px 15px;font-weight: 100;">{{$t('smartIOTDB.block2Title4')}}</h5>
              </div>
              <div style="height: 80%;">
                <ClassLine :id="'classline1'"></ClassLine>
              </div>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 29%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 20%;padding: 15px 0 15px 15px;">
                <h5 style="font-size: 1.25em;color:#E4E9DC;font-weight: 100;">{{$t('smartIOTDB.block2Title5')}}</h5>
              </div>
              <div class="classPercent"> 
                <Row type="flex" justify="center" align="middle" class="classPercent-row">
                  <Col class="left" :span="12" style="">
                    <div style="height: 100%;">
                      <RingPie id="ringpie5'"></RingPie>
                    </div>
                  </Col>
                  <Col class="right" :span="12">
                    <Row>
                      <Col>
                        <div class="right-title">今日總課堂數 : <span>54</span> 堂</div>
                      </Col>
                    </Row>
                    <Row >
                      <Col :span="12">
                        <div class="right-detail">
                          <span class="right-detail-title border-red">一年級開課數</span>
                          <h5 class="right-detail-num">3 <small>(5.5%)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12">
                        <div>
                          <span class="right-detail-title border-green">二年級開課數</span>
                          <h5  class="right-detail-num">13 <small>(24.1%)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12">
                        <div>
                          <span class="right-detail-title border-pink">三年級開課數</span>
                          <h5  class="right-detail-num">4 <small>(7.4%)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12">
                        <div>
                          <span class="right-detail-title border-yellow">四年級開課數</span>
                          <h5  class="right-detail-num">12 <small>(22.2%)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12">
                        <div>
                          <span class="right-detail-title border-blue">五年級開課數</span>
                          <h5  class="right-detail-num">13 <small>(24.1%)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12">
                        <div>
                          <span class="right-detail-title border-orange">六年級開課數</span>
                          <h5  class="right-detail-num">9 <small>(16.7%)</small></h5>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>                
              </div>
            </div>
          </Col>
          <Col  :span="5" class="block-3">
            <div class="block-border" style="height: 23.91%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="today-class-content">
                <h5 class="today-class-content-title">{{$t('smartIOTDB.block3Title1')}}</h5>
                <Row class="today-class-content-contents">
                  <Col :span="12" class="chart"><RingPie :pieData="todayClassData" ref="ringpie1" :id="'ringPie1'"></RingPie></Col>
                  <Col :span="12" class="detail">
                    <Row class="detail-row">
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','新增档案')" @mouseenter="pieHeightLight('class','新增档案')">
                          <span class="detail-title detail-title-border-red">新增档案</span>
                          <h5 class="detail-amount">33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','书面问答')" @mouseenter="pieHeightLight('class','书面问答')">
                          <span  class="detail-title detail-title-border-green">书面问答</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','汇入.pptx')" @mouseenter="pieHeightLight('class','汇入.pptx')">
                          <span  class="detail-title detail-title-border-pink" >汇入.pptx</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','PowerClick')" @mouseenter="pieHeightLight('class','PowerClick')">
                          <span  class="detail-title detail-title-border-yellow" >PowerClick</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','开启.hte')" @mouseenter="pieHeightLight('class','开启.hte')">
                          <span  class="detail-title detail-title-border-blue" >开启.hte</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('class','其他')" @mouseenter="pieHeightLight('class','其他')">
                          <span  class="detail-title detail-title-border-orange" >其他</span>
                          <h5 class="detail-amount" >33% <small>(168)</small></h5>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 2.17%;"></div>
            <div class="block-border" style="height: 23.91%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="today-class-content">
                <h5 class="today-class-content-title">{{$t('smartIOTDB.block3Title2')}}</h5>
                <Row class="today-class-content-contents">
                  <Col :span="12" class="chart"><RingPie :pieData="todayClassVerData" ref="ringpie2" :id="'ringPie2'"></RingPie></Col>
                  <Col :span="12" class="detail">
                    <Row class="detail-row">
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','HiTeach STD')" @mouseenter="pieHeightLight('system','HiTeach STD')">
                          <span class="detail-title detail-title-border-red">HiTeach STD</span>
                          <h5 class="detail-amount">26% <small>(8)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','HiTeach PRE')" @mouseenter="pieHeightLight('system','HiTeach PRE')">
                          <span  class="detail-title detail-title-border-green">HiTeach PRE</span>
                          <h5 class="detail-amount" >25% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','HiTeach PRO')" @mouseenter="pieHeightLight('system','HiTeach PRO')">
                          <span  class="detail-title detail-title-border-pink" >HiTeach PRO</span>
                          <h5 class="detail-amount" >18% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','HiTeach Mobile')" @mouseenter="pieHeightLight('system','HiTeach Mobile')">
                          <span  class="detail-title detail-title-border-yellow" >HiTeach Mobile</span>
                          <h5 class="detail-amount" >10% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','HiTeach TBL')" @mouseenter="pieHeightLight('system','HiTeach TBL')">
                          <span  class="detail-title detail-title-border-blue" >HiTeach TBL</span>
                          <h5 class="detail-amount" >21% <small>(168)</small></h5>
                        </div>
                      </Col>
                      <Col :span="12" class="detail-col">
                        <div @mouseleave="pieDownPlay('system','其他')" @mouseenter="pieHeightLight('system','其他')">
                          <span  class="detail-title detail-title-border-orange" >其他</span>
                          <h5 class="detail-amount" >0% <small>(0)</small></h5>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 2.17%;"></div>
            <div class="block-border" style="height: 47.84%;position: relative;">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 100%;background-color: rgba(217, 217, 217, 0.14);">
                <div style="height: 13%;border-bottom: 1px solid #94998a;padding: 15px 0 0 15px;">
                  <h5 style="font-size: 1.25em;color:#E4E9DC;font-weight: 100;">{{ $t('smartIOTDB.block3Title3')}}</h5>
                </div>
                <div style="height: 87%;border-bottom: 1px solid #94998a;">
                  <ClassBar :id="'classbar1'"></ClassBar>
                </div>
              </div>
            </div>
          </Col>
          <Col  :span="4" class="block-4">
            <div class="block-border" style="height: 10.43%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="hours">
                <span class="hours-title">{{$t('smartIOTDB.block4Title1')}}</span>
                <h5><span>4,234</span>h<span>43</span>m</h5>
              </div>
            </div>
            <div style="height: 2.17%"></div>
            <div class="block-border" style="height: 23.48%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 100%;">
                <h5 style="height: 20%;font-size: 1.25em;color: #E4E9DC;padding: 10px 0 0 10px;font-weight: 100;">{{$t('smartIOTDB.block4Title2')}}</h5>
                <Row type="flex" justify="center" align="middle" style="height: 80%;">
                  <Col style="height: 100%;" :span="12"><RingPie :pieData="smartClassVersion" @highLightInfo="classVersionByPie" :defaultActive="true" :id="'ringPie3'"></RingPie></Col>
                  <Col :span="12" style="padding-left: 7px;color: #94998a;">
                    <span>{{ classVersion.name }}</span>
                    <h5 style="font-weight: 100;font-size: 2.2em;color: #fafafa;">{{ classVersion.percent +$t('unit2')}}</h5>
                    <span>{{$t('smartIOTDB.block4SubTitle1') + ' ' + classVersion.val + ' ' +$t('unit3')}}</span>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 2.17%"></div>
            <div class="block-border" style="height: 23.48%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 100%;">
                <h5 style="height: 20%;font-size: 1.25em;color: #E4E9DC;padding: 10px 0 0 10px;font-weight: 100;">{{$t('smartIOTDB.block4Title3')}}</h5>
                <Row type="flex" justify="center" align="middle" style="height: 80%;">
                  <Col style="height: 100%;" :span="12"><RingPie :pieData="operatingSystems" @highLightInfo="homeworkByPie" :defaultActive="true" :id="'ringPie4'"></RingPie></Col>
                  <Col :span="12" style="padding-left: 7px;color: #94998a;">
                    <span>{{ homework.name }}</span>
                    <h5 style="font-weight: 100;font-size: 2.2em;color: #fafafa;">{{ homework.percent +$t('unit2')}}</h5>
                    <span>{{$t('smartIOTDB.block4SubTitle2') + ' ' + homework.val + ' ' +$t('unit4')}}</span>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 2.17%"></div>
            <div class="block-border" style="height: 36.1%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div class="rank">
                <div class="rank-block">
                  <Row type="flex" justify="center" align="middle" class="rank-block-row">
                    <Col class="sticker" :span="6">
                      <img src="@/assets/image/touxiang.png">
                    </Col>
                    <Col class="conect" :span="18">
                      <span class="conect-title">{{$t('smartIOTDB.block4SubTitle3')}}</span>
                      <h5 class="conect-name ellipsis">Bruse</h5><Icon class="conect-icon" :size="15" type="ios-information-circle" />
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle6')}}: <span class="number">6</span> {{$t('unit1')}}</span>
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle7')}}: <span class="number">139</span> {{$t('unit1')}}</span>
                    </Col>
                  </Row>
                </div>
                <div class="rank-block">
                  <Row type="flex" justify="center" align="middle" class="rank-block-row">
                    <Col class="sticker" :span="6">
                      <img src="@/assets/image/touxiang.png">
                    </Col>
                    <Col class="conect" :span="18">
                      <span class="conect-title">{{$t('smartIOTDB.block4SubTitle4')}}</span>
                      <h5 class="conect-name ellipsis">梁老师</h5><Icon class="conect-icon" :size="15" type="ios-information-circle" />
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle8')}}: <span class="number">256.83</span> {{$t('unit5')}}</span>
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle9')}}: <span class="number">135,873.65</span> {{$t('unit5')}}</span>
                    </Col>
                  </Row>
                </div>
                <div class="rank-block">
                  <Row type="flex" justify="center" align="middle" class="rank-block-row">
                    <Col class="sticker" :span="6">
                      <img src="@/assets/image/touxiang.png">
                    </Col>
                    <Col class="conect" :span="18">
                      <span class="conect-title">{{$t('smartIOTDB.block4SubTitle5')}}</span>
                      <h5 class="conect-name ellipsis">Picc</h5><Icon class="conect-icon" :size="15" type="ios-information-circle" />
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle10')}}: <span class="number">77.8</span> {{$t('unit2')}}</span>
                      <span class="conect-val">{{$t('smartIOTDB.block4SubTitle11')}}: <span class="number">71.1</span> {{$t('unit2')}}</span>
                    </Col>
                  </Row>
                </div>
              </div>
            </div>            
          </Col>
        </Row>
      </div>
    </div>
</template>

<script>
import LegendPie from '@/components/graph/legendPie'
import RingPie from '@/components/graph/ringPie'
import ClassLine from '@/components/graph/classLine'
import ClassBar from '@/components/graph/classBar'

export default {
  name:'smart-dashboard',
  data () {
    return {
      classInfoData:[
        { 
          'className': 'GE305, 通识教室3',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': 'Picc',
          'classHot': '7',
          'classTime': '00:45:03',
          'classMode': 0
        },
        { 
          'className': 'GE305, 通识教室5',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': '梁老师',
          'classHot': '7',
          'classTime': '--:--:--',
          'classMode': 1
        },
        { 
          'className': 'GE305, 通识教室7',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': '布鲁斯',
          'classHot': '7',
          'classTime': '--:--:--',
          'classMode': 2
        },
        { 
          'className': 'GE305, 通识教室3',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': 'Picc',
          'classHot': '7',
          'classTime': '00:45:03',
          'classMode': 0
        },
        { 
          'className': 'GE305, 通识教室5',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': '梁老师',
          'classHot': '7',
          'classTime': '--:--:--',
          'classMode': 1
        },
        { 
          'className': 'GE305, 通识教室7',
          'classProduct': 'Hiteach 3 TBL',
          'classDetail': '序号使用至2021-09-30, 50人, HBI082, RF05H',
          'teacherName': '布鲁斯',
          'classHot': '7',
          'classTime': '--:--:--',
          'classMode': 2
        },
      ],
      scolBeg:{
        percent: '',
        today: '',
        lastWeek: '',
        todayData: [
          {value:9, name:'一年级'},
          {value:11, name:'二年级'},
          {value:16, name:'三年级'},
          {value:6, name:'四年级'},
          {value:13, name:'五年级'},
          {value:22, name:'六年级'},
        ]
      },
      classVersion:{
        percent: '',
        name: '',
        val: 0
      },
      homework:{
        percent: '',
        name: '',
        val: 0
      },
      todayClassData:[
        {value:335, name:'新增档案'},
        {value:234, name:'汇入.pptx'},
        {value:1548, name:'开启.hte'},        
        {value:310, name:'书面问答'},
        {value:135, name:'PowerClick'},
        {value:123, name:'其他'},
      ],
      todayClassVerData:[
        {value:335, name:'HiTeach STD'},
        {value:234, name:'HiTeach PRO'},
        {value:1548, name:'HiTeach TBL'},        
        {value:310, name:'HiTeach PRE'},
        {value:135, name:'HiTeach Mobile'},
        {value:123, name:'其他'},
      ],
      smartClassVersion:[
        {value:11, name:'HiTeach STD'},
        {value:3, name:'HiTeach PRO'},
        {value:16, name:'HiTeach TBL'},        
        {value:6, name:'HiTeach PRE'},
        {value:2, name:'HiTeach Mobile'}
      ],
      operatingSystems:[
        {value:1183, name:'Windows 7'},
        {value:245, name:'Windows XP'},
        {value:473, name:'Windows 10'},
      ]
    }
  },
  components:{
    LegendPie,
    RingPie,
    ClassBar,
    ClassLine
  },
  methods:{
    pieHeightLight: function(type, value) {
      switch (type) {
        case 'class':
          this.$refs.ringpie1.heightlight(value);
          break;
        case 'system':
          this.$refs.ringpie2.heightlight(value);
          break;
      }
    },
    pieDownPlay: function(type, value) {
      switch (type) {
        case 'class':
          this.$refs.ringpie1.downplay(value);
          break;
        case 'system':
          this.$refs.ringpie2.downplay(value);
          break;
      }      
    },
    scolBegInfoByPie: function(val){
      this.scolBeg.percent = val.percent
    },
    scolBegInfoByBar: function(val){
      this.scolBeg.today = val.today
      this.scolBeg.lastWeek = val.lastWeek
      this.$refs.ringpie5.heightlight(val.id);
    },
    classVersionByPie: function(val){
      this.classVersion.percent = val.percent
      this.classVersion.name = val.name
      this.classVersion.val = val.value
    },
    homeworkByPie: function(val){
      this.homework.percent = val.percent
      this.homework.name = val.name
      this.homework.val = val.value
    },
    classModeStr: function(val){
      switch (val) {
        case 0:
          return this.$t('smartIOTDB.runing')
          break;
        case 1:
          return this.$t('smartIOTDB.notInitiated')
          break;
        case 2:
          return this.$t('smartIOTDB.repair')
          break;
      }
    },
    classModeClass: function(val){
      switch (val) {
        case 0:
          return 'processing'
          break;
        case 1:
          return 'unopen'
          break;
        case 2:
          return 'maintain'
          break;
      }
    }
  },
  mounted(){
    var c=document.getElementById("floorplan");
    var ctx=c.getContext("2d");
    var img=document.getElementById("planImg");
    var pin=document.getElementById("pin");
    var greenpin=document.getElementById("greenpin");
    var redpin=document.getElementById("redpin");    
    c.width = img.width;
    c.height = img.height
    var pinW = 35;
    var pinH = 35;
    ctx.drawImage(img,0,0);
    ctx.drawImage(redpin, (c.width*0.595) ,(c.height * 0.67) , pinW, pinH);
    ctx.drawImage(greenpin, (c.width*0.45) ,(c.height * 0.23) , pinW, pinH);
    ctx.drawImage(greenpin, (c.width*0.455) ,(c.height * 0.79) , pinW, pinH);
    ctx.drawImage(greenpin, (c.width*0.595) ,(c.height * 0.79) , pinW, pinH);
    ctx.drawImage(greenpin, (c.width*0.245) ,(c.height * 0.79) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.52) ,(c.height * 0.23) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.59) ,(c.height * 0.23) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.595) ,(c.height * 0.73) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.525) ,(c.height * 0.73) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.525) ,(c.height * 0.79) , pinW, pinH);
    ctx.drawImage(pin, (c.width*0.455) ,(c.height * 0.73) , pinW, pinH);
  }
}
</script>
