<template>
  <div class="login_wrap">
    <div class="form_wrap">
      <span class="welcomeword">欢 迎 登 录</span>
      <el-form ref="formRef" :model="logindata" label-width="100px" class="demo-ruleForm">
        <el-form-item class="txtuser" label="用户名" prop="username" :rules="[
          { required: true, message: '此项不得为空', trigger: 'blur' },
        ]">
          <el-input style="width:250px;" v-model.number="logindata.username" />
        </el-form-item>
        <el-form-item class="txtpassword" label="密码" prop="password" :rules="[
          { required: true, message: '此项不得为空', trigger: 'blur' },
        ]">
          <el-input style="width:250px;" type="password" v-model.number="logindata.password" />
        </el-form-item>
        <div style="display: flex; align-items: center; justify-content: center">
          <el-form-item class="txtcode" label="验证码" prop="code" :rules="[
            { required: true, message: '此项不得为空', trigger: 'blur' },
          ]">
            <el-input style="width: 145px;" type="text" v-model="logincode.inputCode" placeholder="请输入您的验证码" />
          </el-form-item>
          <div @click="refreshCode">
            <SIdentify :identifyCode="identifyCode"></SIdentify>
          </div>
        </div>
        <el-form-item class="btn">
          <el-button type="primary" style="width: 250px;" @click="handleLogin">登录</el-button>
        </el-form-item>
        <!-- <el-form-item>
          <div style="display: flex;">
            <el-input prefix-icon="el-icon-key" v-model="logindata.ValidCode" style="width: 70%;" placeholder="请输入验证码"></el-input>
            <ValicCode @input-="reateValiCode" />
          </div>
        </el-form-item> -->
      </el-form>
    </div>
  </div>
</template>

<script>
import { reactive, toRefs, onMounted, ref } from 'vue';
import { useRouter } from "vue-router"
import { useStore } from "vuex"
import SIdentify from "../components/SIdentify.vue"
export default {
  components: { SIdentify },
  name: "Login",
  setup() {
    const identifyCode = ref('3212')
    const router = useRouter()
    const store = useStore()
    const data = reactive({
      logindata: {
        username: "",
        password: ""
      },
      logincode: {
        identifyCodes: "1234567890",
        inputCode: ""
      }
    })

    const randomNum = (min, max) => {
      return Math.floor(Math.random() * (max - min) + min)
    }

    const makeCode = (o, l) => {
      for (let i = 0; i < l; i++) {
        identifyCode.value += o[
          randomNum(0, o.length)
        ];
      }
    }

    const refreshCode = () => {
      identifyCode.value = "";
      makeCode(data.logincode.identifyCodes, 4);
    }

    onMounted(() => {
      identifyCode.value = "";
      makeCode(data.logincode.identifyCodes, 4);
    })

    const handleLogin = () => {
      if (data.logincode.inputCode == "") {
        alert("请输入验证码");
        return;
      }
      if (data.logincode.inputCode != identifyCode.value) {
        data.logincode.inputCode = "";
        refreshCode;
        alert("请输入正确的验证码");
        return;
      } else {
        store.commit('setUserInfo', data.logindata)
        localStorage.setItem("logindata",JSON.stringify(data.logindata))
        router.push({
          path: "/"
        })
      }

    }
    return {
      ...toRefs(data),
      identifyCode,
      handleLogin,
      refreshCode
    }
  }
}
</script>

<style>
.login_wrap {
  width: 100%;
  height: 100vh;
  background: url('https://cdn.pixabay.com/photo/2018/08/14/13/23/ocean-3605547_1280.jpg') no-repeat;
  background-size: 100% 130%;
  position: relative;
}

.form_wrap {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 20%;
  height: 320px;
  background-color: #f1efefe7;
  border-radius: 2em;
}

.welcomeword {
  font-size: 40px;
  text-align: center;
  color: rgb(63, 61, 61);
}

.txtuser {
  padding-top: 30px;
  margin-left: -20px;
}

.txtpassword {
  margin-left: -20px;
  padding-top: 10px;
}

.txtcode {
  margin-left: -65px;
  margin-top: 14px;
}

.btn {
  margin-left: -30px;
  margin-top: 2%;
}
</style>