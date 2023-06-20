<template>
  <div class="common-layout">
    <el-container>
      <el-header class="common-header flex-float" height="80px">
        <div class="flex">
            <img class="logo" src="../assets/kstopa.png" alt="">
            <h1 class="title">商铺后台管理系统</h1>
        </div>
        <el-button type="primary" @click="loginOut">退出</el-button>
      </el-header>
      <el-container>
        <el-aside class="common-aside" width="200px">
            <el-menu
            background-color="none"
            text-color="#fff"
            :router="true"
            >
                <el-sub-menu index="1">
                    <template #title>
                        <el-icon><DocumentCopy /></el-icon>
                        <span>账号管理</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="/account">账号列表</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu>
                <el-sub-menu index="2">
                    <template #title>
                        <el-icon><User /></el-icon>
                        <span>角色管理</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="/user">角色列表</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu>
                <el-sub-menu index="3">
                    <template #title>
                        <el-icon><Present /></el-icon>
                        <span>商品管理</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="/product">商品列表</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu>
                <el-sub-menu index="4">
                    <template #title>
                        <el-icon><DataAnalysis /></el-icon>
                        <span>数据统计</span>
                    </template>
                    <el-menu-item-group>
                        <el-menu-item index="/displaydata">数据显示</el-menu-item>
                    </el-menu-item-group>
                </el-sub-menu>
            </el-menu>
        </el-aside>
        <el-main class="mainbody">
            <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script>
import { useStore } from "vuex"
import {useRouter} from "vue-router"

export default{
    name:"layout",
    setup(){
        const store=useStore()
        const router=useRouter()
        const loginOut=()=>{
            store.dispatch("authModule/loginOut")
            router.push({
                path:"/login"
            })
        }
        return{
            loginOut
        }
    }

}
</script>

<style>
.common-header{
    background: rgb(49, 49, 58);
    display: flex;
}
.common-aside{
    background: rgb(61, 61, 68);
}
.el-container{
    height: 100vh;
    overflow: hidden;
}
.logo{
    height: 100px;
    width: 200px;
    margin-left: -20px;
}
.title{
    color: white;
    padding-top: 5px;
}
.main{
    background: #e0e0e0ef;
}
</style>
