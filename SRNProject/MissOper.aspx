<%@ Page Title="Miss Operation" Language="vb" AutoEventWireup="false" MasterPageFile="~/SRN.Master" CodeBehind="MissOper.aspx.vb" Inherits="SRNProject.MissOper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
   
<br />
        <div class="se-pre-con"></div>
        <div class="col-lg-12">
            <div class="card border-primary">
                <div class="card-header py-3">
                  <h6 class="m-0"><a href="Menu.aspx">Menu</a><span class="mx-2 mb-0">/</span> <strong class="text-black"><%: Page.Title %></strong>  </h6>
                </div>
                <div class="card-body">
                   
                        
                    <div class="row align-items-center">  
                        <div class="table-responsive">
                        <table id="main_table" class="table table-striped table-bordered" style="width:100%">
                            <thead>
                                <tr>
                                    <th>Seq.</th>
                                    <th>Miss-operation<br/> ID</th>
                                    <th>Order Pick<br/> List NO.</th>
                                    <th>Cust Item</th>
                                    <th>Miss-operation<br/> Label Item</th>
                                    <th>Miss-operation<br/> Transaction Date</th>
                                    <th>Action</th>
                                </tr>
                            </thead>
                            <tbody id="main_body">

                            </tbody>
                        </table>
     
                        </div>
                    </div>

                  
                </div>
            </div>



        </div>

  

 
    <div id="modal_unlock_password" class="modal fade effect-scale">
     <div class="modal-dialog modal-dialog-centered" role="document">
        <div class="modal-content bd-0 tx-14">
            <div class="modal-header pd-y-20 pd-x-25">
              <h6 class="tx-14 mg-b-0 tx-uppercase tx-inverse tx-bold"> Please input pincode for unlock (8 digit)</h6>
              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
            </div>

         

            <div class="modal-body pd-25">
              <div class="row">
                <div class="col-md-12">
                  <input id="tran_id" type="hidden" name="" value="">
                  <div class="form-group">
                    
                    <input id="pin_code" type="password" class="form-control" name="" value="" minlength="8" maxlength="8" required>
                  </div>
                </div>
              </div>
            </div>

            <div class="modal-footer">
              <input type="button" id="Unlock" value="Unlock" onclick="doUnlockProcess()"  class="btn btn-primary tx-11 tx-uppercase pd-y-12 pd-x-25 tx-mont tx-semibold">
            </div>

  

        </div><!-- modal-content -->
       </div><!-- modal-dialog -->
    </div><!-- modal -->

    
        <script>

            $(document).ready(function () {
                getDataMain();
            });


            function doUnlock(id) {

                //Set Value
                $("#pin_code").val('');
                $("#tran_id").val(id);
                $("#modal_unlock_password").modal('show');
              
            }

            
            var input = document.getElementById("pin_code");

            input.addEventListener("keydown", function (event) {

                if (event.keyCode == 13) {
                  event.preventDefault();
                  document.getElementById("Unlock").click();

              }
            });
            

            function doUnlockProcess() {

                var pin_code = $("#pin_code").val();
                var tran_id = $("#tran_id").val();

               

                if (pin_code.length == 8) {

                        $.ajax({
                        url: 'CallMethod.ashx',
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            Token: "<%=Session("Token")%>",
                            Ido: "PPCC_Ex_LogMiss",
                            Method: "PPCC_EX_UnlockMissOperSp",
                            Parms: "<%=Session("UserName")%>,<%=Session("PSite")%>,<%=Session("PSession")%>," + pin_code + "," + tran_id + ""
                            //Parms: "5f7b6a3f-f995-43bf-9536-b99b0596eac8,J,S23BIC0010-0000-10,29/05/2020,MAIN,,0,0,0,I,I,SRN,ip"
                        }

                    }).done(function (data) {

                        //pass true
                        if (data.Stat == "TRUE") {
                            // call process if true
                            $("#modal_unlock_password").modal('hide');
                            Swal.fire({
                                title: 'Unlock',
                                text: "Unlock user successful",
                                type: 'success',
                                onAfterClose: () => location.reload()
                            });
                        //pass false
                        } else {
                            Swal.fire({
                                title: 'Error',
                                text: data.MsgErr,
                                type: 'error',
                            });
                        }

                    }).fail(function (data) {
                        alert("IDO error")
                    });
                } else {
                    Swal.fire('', "Unlock user fail, please input pincode equal 8 digit", 'warning');
                }


                <%--if (pin_code.length == 8) {

                    

                    $.ajax({
                        url: 'CallMethod.ashx',
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            Token: "",
                            Ido: "PPCC_Ex_LogMiss",
                            Method: "PPCC_EX_UnlockMissOperSp",
                            Parms: <%=Session("UserName")%> + "," + <%=Session("PSite")%> + "," + <%=Session("PSession")%> + "," + pin_code + "," + tran_id
                            
                        }
                        
                    }).done(function (data) {
                        
                        //pass true
                        if (data.Stat == "TRUE") {
                            // call process if true
                            $("#modal_unlock_password").modal('hide');
                            Swal.fire({
                                title: 'Unlock',
                                text: "Unlock user successful",
                                type: 'success',
                                onAfterClose: () => location.reload()
                            });
                        //pass false
                        } else {
                            Swal.fire({
                                text: data.MsgErr,
                                type: data.MsgType,
                            });
                        }

                    }).fail(function (data) {
                        alert("IDO error")
                    });

                } else {
                    Swal.fire('', "Unlock user fail, please input pincode equal 8 digit", 'warning');
                }--%>


            }

            
            var main_html = '';

            function getDataMain() {
               
             
                main_html = '';
                url = 'LoadCollection.ashx'
                $.ajax({
                            url: url,
                            type: 'POST',
                            dataType: 'json',
                            data: {
                                    Token: "<%=Session("Token")%>",
                                    Ido: "PPCC_Ex_LogMiss",
                                    Propertie: "TransID,UserMissOper,OrderPickList,CustItem,Item,TransDate",
                                    OrderBy: "CreateDate Desc",
                                    Filter: "UserUnLock Is NULL",
                                    PostQueryMethod: "",
                                    RecordCap:"0"
                            
                                  }
                        }).done(function (data) {
                       
                             var data = data.IDO
                   
                             for (var i = 0; i < data.length; i++) {
                                 main_html += '<tr>';
                                 main_html += '  <td>' + (i+1) + '</td>';
                                 main_html += '  <td>' + data[i]['UserMissOper'] + '</td>';
                                 main_html += '  <td>' + data[i]['OrderPickList'] + '</td>';
                                 main_html += '  <td>' + data[i]['CustItem'] + '</td>';
                                 main_html += '  <td>' + data[i]['Item'] + '</td>';
                                 main_html += '  <td>' + ppcc_datetime(data[i]['TransDate']) + '</td>';
                                 main_html += '  <td><button type="button" title="UnLock" class="btn btn-info bt-sm" name="button" onclick="doUnlock(\'' + data[i]['TransID'] +'\')"><i class="fas fa-unlock"></i></button></td>';
                                 main_html += '</tr>';
                                 
                             }
                        
                             $('#main_body').html(main_html);
                       
                        })
                        .fail(function (data) {
                            console.log(data);

                        });

            }

            function appendLeadingZeroes(n) {
                if (n <= 9) {
                    return "0" + n;
                }
                return n
            }

           


            function ppcc_datetime(__parse) {

                //replace string value from json
                var __d = '';
                var str = __parse;
                str = str.replace(")", "");
                str = str.replace("/", "");
                str = str.replace("/", "");
                __d = str.replace("Date(", "");

                //change format date
                let current_datetime = new Date(parseInt(__d))
                let formatted_date = current_datetime.getFullYear() + "-" + appendLeadingZeroes(current_datetime.getMonth() + 1) + "-" + appendLeadingZeroes(current_datetime.getDate()) + " " + appendLeadingZeroes(current_datetime.getHours()) + ":" + appendLeadingZeroes(current_datetime.getMinutes()) + ":" + appendLeadingZeroes(current_datetime.getSeconds())

                return (formatted_date)
            }


        </script>
</asp:Content>