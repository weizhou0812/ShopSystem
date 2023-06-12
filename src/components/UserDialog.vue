<template>
    <el-dialog v-model="dialogFormVisible" title="添加账号">

        <!-- <template #footer>
            <span class="dialog-footer">
                <el-button @click="dialogFormVisible = false">Cancel</el-button>
                <el-button type="primary" @click="dialogFormVisible = false">
                    Confirm
                </el-button>
            </span>
        </template> -->
    </el-dialog>
</template>


<script>
import { toRefs, reactive } from 'vue'
export default {
    name: "userDialog",
    setup(){
        const data=reactive({
            dialogFormVisible:false
        })

        const dialogDisplay=()=>{
            dialogFormVisible=true
        }
        //主动暴露子组件方法
        defineExpose({dialogDisplay});
        return{  
            ...toRefs(data),
            dialogDisplay
        }
    }
    
    
}
</script>