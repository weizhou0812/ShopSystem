<template>
  <h1>数据统计</h1>
  <div class="data-view">
    <el-card>
      <div ref="barmain" id="barmain"></div>
    </el-card>
    <el-card>
      <div ref="linemain" id="linemain"></div>
    </el-card>
    <el-card>
      <div ref="piemain" id="piemain"></div>
    </el-card>
  </div>
</template>
<script>
import * as echarts from 'echarts';
import { toRefs, reactive, onMounted, ref } from 'vue'
import axios from 'axios';
export default {
  name: "displayData",
  data() {
    return {
      barchanrtInstance: "",
      linechanrtInstance: "",
      piechanrtInstance: "",
      allData: "",
      //pieData:""
    }
  },
  mounted() {
    this.initChart()
    this.getData()
    // axios.get("/Product/selectfiledname")
    //   .then((res) => {
    //     namelist = res.data;
    //     console.log(res.data);
    //   });
    //   axios.get("/Product/selectfiledinventory")
    //   .then((res) => {
    //     datalist = res.data;
    //     console.log(res.data);
    //   });
    // var mybarChart = echarts.init(document.getElementById('barmain'))
    // // 绘制柱状图
    // mybarChart.setOption({
    //   title: {
    //     text: 'ECharts 柱状图'
    //   },
    //   tooltip: {},
    //   xAxis: {
    //     data: namelist
    //   },
    //   yAxis: {},
    //   series: [
    //     {
    //       name: '销量',
    //       type: 'bar',
    //       data: datalist
    //     }
    //   ]
    // });
    // var mylineChart = echarts.init(document.getElementById('linemain'))
    // // 绘制折线图
    // mylineChart.setOption({
    //   title: {
    //     text: 'ECharts 折线图'
    //   },
    //   tooltip: {},
    //   xAxis: {
    //     data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
    //   },
    //   yAxis: {},
    //   series: [
    //     {
    //       name: '销量',
    //       type: 'line',
    //       data: [5, 20, 36, 10, 10, 20]
    //     }
    //   ]
    // });
    // var mypieChart = echarts.init(document.getElementById('piemain'))
    // // 绘制饼图
    // mypieChart.setOption({
    //   title: {
    //     text: 'ECharts 饼图'
    //   },
    //   tooltip: {},
    //   series: [
    //     {
    //       name: '销量',
    //       type: 'pie',
    //       data: [
    //         {
    //           value: 5,
    //           name: '衬衫'
    //         },
    //         {
    //           value: 20,
    //           name: '羊毛衫'
    //         },
    //         {
    //           value: 36,
    //           name: '雪纺衫'
    //         },
    //         {
    //           value: 10,
    //           name: '裤子'
    //         },
    //         {
    //           value: 10,
    //           name: '高跟鞋'
    //         },
    //         {
    //           value: 20,
    //           name: '袜子'
    //         }]
    //     }
    //   ]
    // });
  },

  methods: {
    //初始化echartInstance对象
    initChart() {
      this.barchanrtInstance = echarts.init(this.$refs.barmain)
      this.linechanrtInstance = echarts.init(this.$refs.linemain)
      this.piechanrtInstance = echarts.init(this.$refs.piemain)
    },
    //获取后端数据
    async getData() {
      const { data: barret } = await axios.get("/Product/alldata")
      this.allData = barret
      // const { data: pieret } = await axios.get("/Product/selectfiled")
      // this.pieData = pieret
      this.updateChart()
    },
    //更新图表
    updateChart() {
      const pname = this.allData.map((item) => {
        return item.pName
      })
      const pinventory = this.allData.map((item) => {
        return item.inventory
      })
      const baroption = {
        title: {
          text: '商品柱状图'
        },
        tooltip: {},
        xAxis: {
          data: pname
        },
        yAxis: {},
        series: [
          {
            type: 'bar',
            data: pinventory
          }
        ]
      };
      this.barchanrtInstance.setOption(baroption)
      const lineoption = {
        title: {
          text: '商品折线图'
        },
        tooltip: {},
        xAxis: {
          type: 'category',
          data: pname
        },
        yAxis: {
          type: 'value'
        },
        series: [
          {
            data: pinventory,
            type: 'line'
          }
        ]
      };
      this.linechanrtInstance.setOption(lineoption)
      const piedata = this.allData.map(item => {
        return {
          name: item.pName,
          value: item.inventory
        }
      })
      const pieoption = {
        title: {
          text: '商品饼图'
        },
        tooltip: {},
        series: [
          {
            type: 'pie',
            data: piedata,
            radius: '50%'
          }
        ]
      };
      this.piechanrtInstance.setOption(pieoption)
    }
  }
}
</script>
<style>
.data-view {
  width: 100%;
  display: flex;
  justify-content: space-between;

  .el-card {
    width: 33.3%;

    #barmain,
    #linemain,
    #piemain {
      height: 500px;
    }
  }
}
</style>