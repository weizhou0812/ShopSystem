// export default{
//     state:{
//         userInfo:(localStorage.getItem("logindata")&&
//         JSON.parse(localStorage.getItem("logindata")))||{}
//     },
//     mutations:{
//         setUserInfo(state,uInfo){
//             state.userInfo=uInfo
//         }
//     }
// }
import router from "@/router";
import { loginUser,logOut } from "@/views/auth.service";
const authModule={
    //namespaced告诉使用者调用时需要加上命名空间
    namespaced:true,
    state:{
        //全局变量
        signInState:{
            uName:'',
            exp:Date.now(),
            sub:"",
            token:null,
        }

    },
    getters:{
    },
    mutations:{
        //修改全局变量
        //修改token的状态
        userLogin(state,token){
            state.signInState.token=token
            localStorage.setItem("tokenAnt",token)
        }
    },
    actions:{
        //异步修改全局变量
        async userLoginAction({commit},login){
        const {data}= await loginUser(login)
        console.log(data)
        commit('userLogin',data.token)
        router.replace("/")
        },
        loginOut(){
            //移除token
            logOut();
            
        }
    },
}
export default authModule;