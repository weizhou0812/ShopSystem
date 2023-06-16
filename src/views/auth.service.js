import router from "@/router"
import axios from "./api_config"
import * as jwt from "jsonwebtoken"
//登录
export const  loginUser=async(login)=>{
    return await axios.post("Login/Auth",login)
}
const key="tokenAnt"
//从浏览器获取token
export const getToken=()=>{
    console.log(localStorage.getItem(key))
    return localStorage.getItem(key)
} 
//清除token
export const logOut=()=>{
    //localStorage.clear()
    localStorage.removeItem(key)
}
//检查token过期时间
export const isTokenFromLocalStorageVaild=()=>{
    const token =localStorage.getItem(key)
    if(!token)return false
    const decoded=jwt.decode(token)
    //时间戳分为10位（秒级）和十三位（毫秒级）
    const dateNow=Date.now()
    const expiresAt=decoded.exp*1000
    return dateNow<=expiresAt
}