/*global $*/

function saveOpinion(){
 
  $.ajax({
       //url: '/opinions',
      //type: 'POST',
       success: function(data) {
           data.name=$("#name").val();
           data.opinion=$("#opinion").val();
           $('#opinions').append(data.id+data.name+data.opinion);
           goBack();
           
       }
   });
}

function goBack() {
    window.history.back();
}



function deleteOpinion(id) {
    $.ajax({
        url: '/opinions/'+id,
        type: 'DELETE', 
        success: function(data) {
            $('#row_id_'+id).remove();
        }
    });
}




$(document).ready(function(){
                $.ajax({url:'/opinions',
        
        success: function(data) {
                    for(var i = 0; i<data.length;i++) {
                        $('#opinions').append("<p>"+"-" +data[i].name+":   "+data[i].opinion+ "</p><button onclick='deleteOpinion("+data[i].id+
                        ")'> Sterge</button>");
                    }
                }});
            });


 
