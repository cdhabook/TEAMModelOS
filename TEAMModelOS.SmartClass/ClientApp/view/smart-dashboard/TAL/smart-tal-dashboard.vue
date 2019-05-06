<style lang="less" scoped>
  @import './smart-tal-dashboard.less';
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

        </div>
        <Row class="box" type="flex" justify="center">
          <Col  :span="11"  class="block-1">
            <div style="height: 100%;">
              <div class="block-border" style="height: 66%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
                <!-- 边角设计 start-->
                <div class="image-border image-border-left-top" ></div>
                <div class="image-border image-border-right-top" ></div>
                <div class="image-border image-border-left-bottom" ></div>
                <div class="image-border image-border-lright-bottom" ></div>
                <!-- 边角设计 end-->
                <div style="height: 100%;">
                  <Row type="flex" justify="center" align="middle" style="height: 25%;">
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">收课总数</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">7,732</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">教师数</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">108</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">特级典藏数</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">1,381</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">高级典藏数</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">2,569</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">一级典藏数</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">3,782</h5>
                    </Col>
                  </Row>
                  <Row type="flex" justify="center" align="middle" style="height: 25%;">
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">本日</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">10</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">近七日</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">88</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">近30日</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">474</h5>
                    </Col>
                    <Col :span="4" style="text-align: center;">
                      <span style="font-size: 1.25em;color: rgb(148, 153, 138)">本学期</span>
                      <h5 style="font-size: 2.5em;color: rgb(250, 250, 250);font-weight: 100;">1,530</h5>
                    </Col>
                  </Row>
                  <Row style="height: 50%">
                    <Col :span="12" style="height: 100%;">
                      <RingPie :tooltip="true" :title="'年级'" :pieData="[ {value:3482, name:'一年级'},{value:2137, name:'二年级'},{value:1302, name:'三年级'},{value:805, name:'四年级'},{value:6, name:'五年级'}]" :id="'ringpie1'"></RingPie>
                    </Col>
                    <Col :span="12" style="height: 100%;">
                      <RingPie :tooltip="true" :title="'科目'" :pieData="[ {value:1958, name:'语文'},{value: 1396, name:'数学'},{value: 469, name:'英文'},{value: 206, name:'科学'},{value: 332, name:'艺术'},{value: 2087, name:'其他'}]" :id="'ringpie2'"></RingPie>
                    </Col>
                  </Row>
                </div>
              </div>  
              <div style="height: 1.5%"></div>
              <div style="height: 30.5%;">
                <Row :gutter="16" type="flex" justify="space-between" align="middle" style="height: 100%;">
                  <Col :span="12" style="height: 100%;">
                    <div class="block-border" style="position: relative;background-color: rgba(217, 217, 217, 0.14);height: 100%;">
                      <!-- 边角设计 start-->
                      <div class="image-border image-border-left-top" ></div>
                      <div class="image-border image-border-right-top" ></div>
                      <div class="image-border image-border-left-bottom" ></div>
                      <div class="image-border image-border-lright-bottom" ></div>
                      <!-- 边角设计 end-->
                      <Bubble style="padding-right: 16px;" 
                        :bubleData="[ [0,0,1564], [1,0, 404], [2,0, 436], [3,0, 579], [4,0, 467],[5,0, 32],
                                      [0,1,638], [1,1, 224], [2,1, 328], [3,1, 515], [4,1, 408],[5,1, 24],
                                      [0,2,394], [1,2, 169], [2,2, 199], [3,2, 241], [4,2, 286],[5,2, 13],
                                      [0,3,445], [1,3, 87], [2,3, 97], [3,3, 95], [4,3, 81],[5,3, 1],
                                      [0,4,1], [1,4, 0], [2,4, 0], [3,4, 0], [4,4, 4],[5,4, 0],]"
                        :title="'科技互动指数关系图(年级)'"
                        :id="'bubble1'"></Bubble>
                    </div>
                  </Col>
                  <Col :span="12" style="height: 100%;">
                    <div class="block-border" style="position: relative;background-color: rgba(217, 217, 217, 0.14);height: 100%;">
                      <!-- 边角设计 start-->
                      <div class="image-border image-border-left-top" ></div>
                      <div class="image-border image-border-right-top" ></div>
                      <div class="image-border image-border-left-bottom" ></div>
                      <div class="image-border image-border-lright-bottom" ></div>
                      <!-- 边角设计 end-->
                      <Bubble style="padding-right: 16px;"
                         :bubleData="[  [0,0,78], [1,0, 20], [2,0, 41], [3,0, 46], [4,0, 19],[5,0, 0],
                                        [0,1,190], [1,1, 0], [2,1, 0], [3,1, 0], [4,1, 0],[5,1, 0],
                                        [0,2,776], [1,2, 241], [2,2, 294], [3,2, 361], [4,2, 275],[5,2, 0],
                                        [0,3,249], [1,3, 53], [2,3, 38], [3,3, 57], [4,3, 66],[5,3, 0],
                                        [0,4,144], [1,4, 27], [2,4, 32], [3,4, 52], [4,4, 73],[5,4, 0],
                                        [0,5,57], [1,5, 20], [2,5, 44], [3,5, 54], [4,5, 30],[5,5, 0],
                                        [0,6,154], [1,6, 49], [2,6, 87], [3,6, 177], [4,6, 280],[5,6, 21],
                                        [0,7,586], [1,7, 160], [2,7, 195], [3,7, 224], [4,7, 218],[5,7, 13],
                                        [0,8,56], [1,8, 0], [2,8, 15], [3,8, 28], [4,8, 13],[5,8, 0],
                                        [0,9,592], [1,9, 271], [2,9, 279], [3,9, 369], [4,9, 202],[5,9, 11],
                                      ]"
                      :title="'科技互动指数关系图(科目)'" 
                      :ylabel="['音乐', '软件测试', '语文', '英语', '美术', '科学', '班会', '数学', '开学课程', '其他']" :id="'bubble2'"></Bubble>
                    </div>  
                  </Col> 
                </Row>
              </div>  
            </div>
          </Col>
          <Col  :span="11" class="block-2">
            <!-- block-2 -->
            <div class="block-border" style="height: 38%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <div style="height: 100%;">
                <Row type="flex" justify="center" align="middle" style="height: 25%;">
                  <Col :span="4" style="text-align: center;">
                    <span style="font-size: 1.25em;color: rgb(148, 153, 138)">本日互动/教法</span>
                    <h5 style="font-size: 2.3em;color: rgb(250, 250, 250);font-weight: 100;">33/50</h5>
                  </Col>
                  <Col :span="4" style="text-align: center;">
                    <span style="font-size: 1.25em;color: rgb(148, 153, 138)">30日互动/教法</span>
                    <h5 style="font-size: 2.3em;color: rgb(250, 250, 250);font-weight: 100;">46/47</h5>
                  </Col>
                  <Col :span="4" style="text-align: center;">
                    <span style="font-size: 1.25em;color: rgb(148, 153, 138)">本学期互动/教法</span>
                    <h5 style="font-size: 2.3em;color: rgb(250, 250, 250);font-weight: 100;">46/45</h5>
                  </Col>
                  <Col :span="4" style="text-align: center;">
                    <span style="font-size: 1.25em;color: rgb(148, 153, 138)">所有互动/教法</span>
                    <h5 style="font-size: 2.3em;color: rgb(250, 250, 250);font-weight: 100;">62/57</h5>
                  </Col>
                </Row>
                <div style="height: 5%;"></div>
                <Row  type="flex" justify="space-around" align="middle" style="height: 70%">
                  <Col :span="11" style="height: 100%;">
                    <InteractiveLine :id="'interactiveline1'"></InteractiveLine>
                  </Col>
                  <Col :span="11" style="height: 100%;">
                    <BnteractiveBarLine :id="'BnteractiveBarLine1'"></BnteractiveBarLine>
                  </Col>
                </Row>
              </div>
            </div>
            <div style="height: 1.5%"></div>
            <div class="block-border" style="height: 26.5%;position: relative;background-color: rgba(217, 217, 217, 0.14);">
              <!-- 边角设计 start-->
              <div class="image-border image-border-left-top" ></div>
              <div class="image-border image-border-right-top" ></div>
              <div class="image-border image-border-left-bottom" ></div>
              <div class="image-border image-border-lright-bottom" ></div>
              <!-- 边角设计 end-->
              <TechnologyBar :id="'234'"></TechnologyBar>
            </div>
            <div style="height: 1.5%"></div>
            <div style="height: 30.5%;">
              <Row :gutter="16" type="flex" justify="space-between" align="middle" style="height: 100%;">
                <Col :span="12" style="height: 100%;">
                  <div class="block-border" style="position: relative;background-color: rgba(217, 217, 217, 0.14);height: 100%;">
                    <!-- 边角设计 start-->
                    <div class="image-border image-border-left-top" ></div>
                    <div class="image-border image-border-right-top" ></div>
                    <div class="image-border image-border-left-bottom" ></div>
                    <div class="image-border image-border-lright-bottom" ></div>
                    <!-- 边角设计 end-->
                    <Bubble style="padding-right: 16px;" 
                      :bubleData="[ [0,0,2039], [1,0, 380], [2,0, 479], [3,0, 365], [4,0, 212],[5,0,7],
                                    [0,1,965], [1,1, 298], [2,1, 357], [3,1, 328], [4,1, 181],[5,1, 8],
                                    [0,2,581], [1,2, 158], [2,2, 219], [3,2, 197], [4,2, 138],[5,2, 9],
                                    [0,3,589], [1,3, 73], [2,3, 70], [3,3, 42], [4,3, 31],[5,3, 0],
                                    [0,4,1], [1,4, 0], [2,4, 0], [3,4, 1], [4,4, 4],[5,4, 4],]"
                      :title="'教法应用指数关系图(年级)'" 
                      :id="'bubble3'"></Bubble>                  
                  </div>
                </Col>
                <Col :span="12" style="height: 100%;">
                  <div class="block-border" style="position: relative;background-color: rgba(217, 217, 217, 0.14);height: 100%;">
                    <!-- 边角设计 start-->
                    <div class="image-border image-border-left-top" ></div>
                    <div class="image-border image-border-right-top" ></div>
                    <div class="image-border image-border-left-bottom" ></div>
                    <div class="image-border image-border-lright-bottom" ></div>
                    <!-- 边角设计 end-->
                    <Bubble style="padding-right: 16px;" 
                     :bubleData="[  [0,0,121], [1,0, 36], [2,0, 29], [3,0, 13], [4,0, 5],[5,0, 0],
                                    [0,1,189], [1,1, 2], [2,1, 0], [3,1, 1], [4,1, 0],[5,1, 0],
                                    [0,2,1160], [1,2, 258], [2,2, 277], [3,2, 194], [4,2, 65],[5,2, 4],
                                    [0,3,345], [1,3, 36], [2,3, 50], [3,3, 29], [4,3, 9],[5,3, 0],
                                    [0,4,195], [1,4, 44], [2,4, 49], [3,4, 38], [4,4, 6],[5,4, 0],
                                    [0,5,96], [1,5, 26], [2,5, 35], [3,5, 38], [4,5, 11],[5,5, 0],
                                    [0,6,302], [1,6, 127], [2,6, 145], [3,6, 138], [4,6, 55],[5,6, 1],
                                    [0,7,837], [1,7, 173], [2,7, 174], [3,7, 156], [4,7, 56],[5,7, 0],
                                    [0,8,82], [1,8, 13], [2,8, 11], [3,8, 9], [4,8, 5],[5,8, 0],
                                    [0,9,638], [1,9, 156], [2,9, 307], [3,9, 275], [4,9, 330],[5,9, 18],
                                  ]"
                      :title="'教法应用指数关系图(科目)'" 
                      :ylabel="['音乐', '软件测试', '语文', '英语', '美术', '科学', '班会', '数学', '开学课程', '其他']" :id="'bubble4'"></Bubble>
                  </div>
                </Col> 
              </Row>
            </div>
          </Col>
        </Row>
      </div>
    </div>
</template>

<script>
import InteractiveLine from '@/components/graph/interactiveLine'
import BnteractiveBarLine from '@/components/graph/interactiveBarLine'
import TechnologyBar from '@/components/graph/technologyBar'
import RingPie from '@/components/graph/ringPie'
import Bubble from '@/components/graph/bubble'

export default {
  name:'smart-dashboard',
  data () {
    return {
    
    }
  },
  components:{
    InteractiveLine,
    TechnologyBar,
    Bubble,
    RingPie,
    BnteractiveBarLine,
  },
  methods:{   
  }
}
</script>
