<template>
    <div>
        <el-breadcrumb :separator-icon="ArrowRight">
            <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
            <el-breadcrumb-item>商品管理</el-breadcrumb-item>
            <el-breadcrumb-item>商品列表</el-breadcrumb-item>
        </el-breadcrumb>
    </div>
    <div class="connet">
        <el-input placeholder="请输入关键字" class="input-with-select" v-model="inputuid" clearable
            style="width: 200px;margin-right: 15px;margin-left: -1190px;">
            <template #append>
                <el-button @click="handlesearch"><el-icon>
                        <Search />
                    </el-icon></el-button>
            </template>
        </el-input>
        <el-button type="primary" @click="handleadd">添加用户</el-button>
        <el-button type="primary" @click="handlereflesh">刷新数据</el-button>
    </div>
    <div class="employeelist">
        <!-- Table表格 
        data：要展示的数组数据
        prop：每条数据对应的属性
        lable：类标题
        scope.row：相当于一条数据
        -->
        <el-table :data="list" border style="width: 100%">
            <el-table-column prop="pId" label="商品编号" />
            <el-table-column prop="pName" label="商品名称" />
            <el-table-column prop="price" label="商品价格/元" />
            <el-table-column prop="sellbyDate" label="保质期/月" />
            <el-table-column prop="produceDate" label="生产日期" />
            <el-table-column prop="warehousingDate" label="入库时间" />
            <el-table-column prop="inventory" label="库存/箱" />
            <el-table-column prop="isExpire" label="是否过期">
                <template v-slot="scoped">
                    <el-switch v-model="scoped.row.isExpire"></el-switch>
                </template>
            </el-table-column>
            <el-table-column label="操作" width="150px">
                <template #default="scope">
                    <el-button type="primary" @click="handleedit(scope.row)"><el-icon>
                            <Edit />
                        </el-icon></el-button>
                    <el-button type="danger" @click="handledelete(scope.row.pId)"><el-icon>
                            <Delete />
                        </el-icon></el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-pagination background style="margin-top: 10px;" layout=" prev, pager, next, jumper, sizes,->, total"
            v-model:current-page="paging.pagenum" v-model:page-size="paging.pagesize" :page-sizes="[5, 10, 20, 50]"
            :total="total" @size-change="handleSizeChange" @current-change="handleCurrentChange" />
    </div>
    <el-dialog v-model="dialogFormVisible" :title="dialogTitle" :tableRow="tableRow" width="30%">
        <el-form ref="productForm" :model="addProductData" :rules="rules">
            <el-form-item label="商品编号" prop="pId">
                <el-input v-model="addProductData.pId" placeholder="请输入商品编号" />
            </el-form-item>
            <el-form-item label="商品名称" prop="pName">
                <el-input v-model="addProductData.pName" placeholder="请输入商品名称" />
            </el-form-item>
            <el-form-item label="商品单价" prop="price">
                <el-input v-model="addProductData.price" placeholder="请输入商品单价" />
            </el-form-item>
            <el-form-item label="保质期/月" prop="sellbyDate">
                <el-input v-model="addProductData.sellbyDate" placeholder="请输入商品保质期" />
            </el-form-item>
            <el-form-item label="生产日期" prop="produceDate">
                <el-date-picker v-model="addProductData.produceDate" type="datetime" placeholder="请填写生产日期" />
            </el-form-item>
            <el-form-item label="入库时间" prop="warehousingDate">
                <el-date-picker v-model="addProductData.warehousingDate" type="datetime" placeholder="请填写入库时间" />
            </el-form-item>
            <el-form-item label="库存" prop="inventory">
                <el-input v-model="addProductData.inventory" placeholder="请输入商品库存" />
            </el-form-item>
            <!-- <el-form-item label="是否过期" prop="isExpire">
                <el-switch v-model="addProductData.isExpire" />
            </el-form-item> -->
        </el-form>
        <template #footer>
            <span class="dialog-footer">
                <el-button type="primary" @click="addProduct(productForm)">确定</el-button>
                <el-button @click="dialogFormVisible = false">取消</el-button>
            </span>
        </template>
    </el-dialog>
</template>

<script>
import { ArrowRight } from '@element-plus/icons-vue'
import { toRefs, reactive, onMounted, ref } from 'vue'
import axios from '../api_config'
import { ElMessage, ElMessageBox } from 'element-plus'
export default {
    name: "productManage",
    setup() {
        const tableData = reactive(
            {
                list: [],
                dialogFormVisible: false,
                paging: {
                    query: "",
                    pagesize: 5,
                    pagenum: 1
                },
                addProductData: {
                    pId: "",
                    pName: "",
                    price: "",
                    sellbyDate: "",
                    produceDate: ref(''),
                    warehousingDate: ref(''),
                    inventory: "",
                    isExpire: false
                },
                rules: {
                    pId: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    pName: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    price: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    sellbyDate: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                    inventory: [
                        { required: true, message: "此项不得为空", trigger: "blur" }
                    ],
                },
                total: "",
                dialogTitle: "",
                inputuid: ""
            }

        );

        const getAlllist = () => {
            axios.get("/Product/getlistbypage?pageNum=" + tableData.paging.pagenum + "&&pageSize=" + tableData.paging.pagesize,)
                .then((res) => {
                    tableData.list = res.data;
                    console.log(res.data);
                });
            axios.get("/Product/Count")
                .then((res) => {
                    tableData.total = res.data
                    console.log(res.data)
                });
        };

        getAlllist();

        const handlesearch = () => {
            axios.get("/Product/FuzzyQuery?pid=" + tableData.inputuid + "&&pageNum=" + tableData.paging.pagenum + "&&pageSize=" + tableData.paging.pagesize)
                .then((res) => {
                    tableData.list = res.data;
                });
            axios.get("/Product/FQCount?pid=" + tableData.inputuid)
                .then((res) => {
                    tableData.total = res.data
                });
        };
        const handleadd = () => {
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "添加信息"
        };
        const handlereflesh = () => {
            tableData.inputuid = '';
            getAlllist();
        };
        const handleedit = (row) => {
            console.log(row)
            tableData.dialogFormVisible = true
            tableData.dialogTitle = "修改信息"
            tableData.addProductData = row
        };
        const handledelete = (pId) => {
            ElMessageBox.confirm(
                '你确定要删除此条信息吗？',
                '消息提示',
                {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning',
                }
            )
                .then(() => {
                    axios.delete("/Product/byid?pId=" + pId)
                    ElMessage({
                        type: 'success',
                        message: '删除成功',
                    })
                    getAlllist();
                })
                .catch(() => {
                    ElMessage({
                        type: 'info',
                        message: '删除取消',
                    })
                })
        };
        const addProduct = (form) => {
            if (tableData.dialogTitle === "添加信息") {
                //validate:	对整个表单的内容进行验证。
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.post("/Product", tableData.addProductData).then(() => {
                        ElMessage.success("添加成功")
                        tableData.dialogFormVisible = false
                        tableData.addProductData = {
                            pId: "",
                            pName: "",
                            price: "",
                            sellbyDate: "",
                            produceDate: ref(''),
                            warehousingDate: ref(''),
                            inventory: "",
                            isExpire: false
                        }
                        getAlllist();
                    })
                })
            } else {
                form.validate(res => {
                    if (!res) {
                        return
                    }
                    axios.put("/Product", tableData.addProductData).then(() => {
                        ElMessage.success("修改成功")
                        tableData.dialogFormVisible = false
                        tableData.addProductData = {
                            pId: "",
                            pName: "",
                            price: "",
                            sellbyDate: "",
                            produceDate: ref(''),
                            warehousingDate: ref(''),
                            inventory: "",
                            isExpire: false
                        }
                    })
                })
            }
        }
        const handleSizeChange = (val) => {
            tableData.paging.pagesize = val
            if (tableData.inputuid === "") { getAlllist(); }
            else {
                handlesearch();
            }
        }
        const handleCurrentChange = (val) => {
            tableData.paging.pagenum = val
            if (tableData.inputuid === "") { getAlllist(); }
            else {
                handlesearch();
            }
        }
        const productForm = ref()
        //const createdate = ref('')
        return {
            ...toRefs(tableData),
            handlesearch,
            handleadd,
            handlereflesh,
            handleedit,
            handledelete,
            addProduct,
            productForm,
            getAlllist,
            handleSizeChange,
            handleCurrentChange,

        };
    },
}
</script>
<style>
.connet {
    box-sizing: border-box;
    display: block;
    width: 100%;
    padding: 20px;
    background: #ffff;
}
</style>
