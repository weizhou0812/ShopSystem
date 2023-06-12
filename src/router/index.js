import { createRouter, createWebHashHistory } from 'vue-router'
import LayOut from '../views/LayOut.vue'
import Login from '../views/Login.vue'
import store from "../store/index.js"
const routes = [
  {
    path:"/login",
    name:"login",
    component:Login
  },
  {
    path: '/',
    name: 'layout',
    component: LayOut,
    redirect:"/index",
    //子路由
    children:[
      {
        path:"/index",
        name:"index",
        component:()=>import("../views/Pages/index.vue")
      },
      {
        path:"/user",
        name:"user",
        component:()=>import("../views/Pages/userManage.vue")
      },
      {
        path:"/account",
        name:"account",
        component:()=>import("../views/Pages/accountManage.vue")
      },
      {
        path:"/product",
        name:"product",
        component:()=>import("../views/Pages/productManage.vue")
      },
      // {
      //   path:"/userDialog",
      //   name:"userDialog",
      //   component:()=>import("../components/UserDialog.vue")
      // }
    ]
  },

]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

router.beforeEach((to,from,next)=>{
  /**
   * to:从哪个页面
   * from：到那个页面
   * next：只有执行了next()页面才会进行跳转
   */
  console.log("store",store.state.uInfo)
  const uInfo=store.state.uInfo.userInfo
  if(!uInfo.username){
    //未登录
    if(to.path=="/login"){
      next()
      return
    }
    next("/login")
  }else{
    next()
  }
})

//暴露路由对象
export default router
