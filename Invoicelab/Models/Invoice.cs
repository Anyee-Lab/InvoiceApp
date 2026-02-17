using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InvoiceLib.Models;

public class Invoice
{
    /// <summary>
    /// 唯一的log id，用于问题定位
    /// </summary>
    public ulong log_id { get; set; }

    /// <summary>
    /// 传入PDF文件的总页数，当 pdf_file 参数有效时返回该字段
    /// </summary>
    public uint pdf_file_size { get; set; }

    /// <summary>
    /// 传入OFD文件的总页数，当 ofd_file 参数有效时返回该字段
    /// </summary>
    public string? ofd_file_size { get; set; }

    /// <summary>
    /// 识别结果数，表示words_result的元素个数
    /// </summary>
    public uint words_result_num { get; set; }

    /// <summary>
    /// 识别结果
    /// </summary>
    public WordsResult? words_result { get; set; }
}
public class WordsResult
{
    /// <summary>
    /// 发票消费类型。不同消费类型输出：餐饮、电器设备、通讯、服务、日用品食品、医疗 、交通、其他
    /// </summary>
    public string? ServiceType { get; set; }



    /// <summary>
    /// 发票种类。不同类型发票输出：普通发票、专用发票、电子普通发票、电子专用发票、 通行费电子普票、区块链发票、通用机打电子发票、电子发票(专用发票)、电子发票(普通发票)
    /// </summary>
    public string? InvoiceType { get; set; }

    /// <summary>
    /// 发票名称
    /// </summary>
    public string? InvoiceTypeOrg { get; set; }

    /// <summary>
    /// 发票代码
    /// </summary>
    public string? InvoiceCode { get; set; }

    /// <summary>
    /// 发票号码
    /// </summary>
    public string? InvoiceNum { get; set; }

    /// <summary>
    /// 发票代码的辅助校验码，一般业务情景可忽略
    /// </summary>
    public string? InvoiceCodeConfirm { get; set; }

    /// <summary>
    /// 发票号码的辅助校验码，一般业务情景可忽略
    /// </summary>
    public string? InvoiceNumConfirm { get; set; }

    /// <summary>
    /// 数电票号，仅针对纸质的全电发票，在密码区有数电票号码的字段输出
    /// </summary>
    public string? InvoiceNumDigit { get; set; }

    /// <summary>
    /// 增值税发票左上角标志。 包含：通行费、销项负数、代开、收购、成品油、其他
    /// </summary>
    public string? InvoiceTag { get; set; }

    /// <summary>
    /// 机打号码。仅增值税卷票含有此参数
    /// </summary>
    public string? MachineNum { get; set; }

    /// <summary>
    /// 机器编号。仅增值税卷票含有此参数
    /// </summary>
    public string? MachineCode { get; set; }

    /// <summary>
    /// 校验码
    /// </summary>
    public string? CheckCode { get; set; }

    /// <summary>
    /// 开票日期
    /// </summary>
    public string? InvoiceDate { get; set; }

    /// <summary>
    /// 购方名称
    /// </summary>
    public string? PurchaserName { get; set; }

    /// <summary>
    /// 购方纳税人识别号
    /// </summary>
    public string? PurchaserRegisterNum { get; set; }

    /// <summary>
    /// 购方地址及电话
    /// </summary>
    public string? PurchaserAddress { get; set; }

    /// <summary>
    /// 购方开户行及账号
    /// </summary>
    public string? PurchaserBank { get; set; }

    /// <summary>
    /// 密码区
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 省
    /// </summary>
    public string? Province { get; set; }

    /// <summary>
    /// 市
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 联次信息。专票第一联到第三联分别输出：第一联：记账联、第二联：抵扣联、第三联：发票联；普通发票第一联到第二联分别输出：第一联：记账联、第二联：发票联
    /// </summary>
    public string? SheetNum { get; set; }

    /// <summary>
    /// 是否代开
    /// </summary>
    public string? Agent { get; set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    public RowWordPair[]? CommodityName { get; set; }

    /// <summary>
    /// 规格型号
    /// </summary>
    public RowWordPair[]? CommodityType { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public RowWordPair[]? CommodityUnit { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public RowWordPair[]? CommodityNum { get; set; }

    /// <summary>
    /// 单价
    /// </summary>
    public RowWordPair[]? CommodityPrice { get; set; }

    /// <summary>
    /// 金额
    /// </summary>
    public RowWordPair[]? CommodityAmount { get; set; }

    /// <summary>
    /// 税率
    /// </summary>
    public RowWordPair[]? CommodityTaxRate { get; set; }

    /// <summary>
    /// 税额
    /// </summary>
    public RowWordPair[]? CommodityTax { get; set; }

    /// <summary>
    /// 车牌号。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    public RowWordPair[]? CommodityPlateNum { get; set; }

    /// <summary>
    /// 类型。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    public RowWordPair[]? CommodityVehicleType { get; set; }

    /// <summary>
    /// 通行日期起。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    public RowWordPair[]? CommodityStartDate { get; set; }

    /// <summary>
    /// 通行日期止。仅通行费增值税电子普通发票含有此参数
    /// </summary>
    public RowWordPair[]? CommodityEndDate { get; set; }

    /// <summary>
    /// 电子支付标识。仅区块链发票含有此参数
    /// </summary>
    public string? OnlinePay { get; set; }

    /// <summary>
    /// 销售方名称
    /// </summary>
    public string? SellerName { get; set; }

    /// <summary>
    /// 销售方纳税人识别号
    /// </summary>
    public string? SellerRegisterNum { get; set; }

    /// <summary>
    /// 销售方地址及电话
    /// </summary>
    public string? SellerAddress { get; set; }

    /// <summary>
    /// 销售方开户行及账号
    /// </summary>
    public string? SellerBank { get; set; }

    /// <summary>
    /// 合计金额
    /// </summary>
    public string? TotalAmount { get; set; }

    /// <summary>
    /// 合计税额
    /// </summary>
    public string? TotalTax { get; set; }

    /// <summary>
    /// 价税合计(大写)
    /// </summary>
    public string? AmountInWords { get; set; }

    /// <summary>
    /// 价税合计(小写)
    /// </summary>
    public string? AmountInFiguers { get; set; }

    /// <summary>
    /// 收款人
    /// </summary>
    public string? Payee { get; set; }

    /// <summary>
    /// 复核
    /// </summary>
    public string? Checker { get; set; }

    /// <summary>
    /// 开票人
    /// </summary>
    public string? NoteDrawer { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remarks { get; set; }

    /// <summary>
    /// 判断是否存在印章。返回“0或1”，1代表存在印章，0代表不存在印章，当 seal_tag=true 时返回该字段
    /// </summary>
    public string? company_seal { get; set; }

    /// <summary>
    /// 印章识别结果内容。当 seal_tag=true 时返回该字段
    /// </summary>
    public string? seal_info { get; set; }
}

public class RowWordPair
{
    /// <summary>
    /// 行号
    /// </summary>
    public string? row { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string? word { get; set; }

}