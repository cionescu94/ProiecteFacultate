var express = require("express");
var bodyParser = require("body-parser");
var cors = require("cors");
var Sequelize=require("sequelize");

var app = express();
app.use(bodyParser.json());
app.use(cors());


var nodeadmin=require('nodeadmin');
app.use(nodeadmin(app));


var sequelize = new Sequelize('opinionsdb', 'c_ionescu', '',{
  dialect: 'mysql', //ce tip de bd folosesc
   host:'127.0.0.1',
   port: 3306
});


var Opinion=sequelize.define('opinion',{
    id:{
        type:Sequelize.INT,
        field:'id'
    },
    name:{
        type:Sequelize.STRING,
        field:'name'
        
    },
    opinion:{
        type:Sequelize.STRING,
        field:'opinion'
    }
},{
     timestamps: false
});




// REST methods
app.get('/opinions', function(req,res){
    Opinion.findAll().then(function(opinions){
    res.status(200).send(opinions); });
});


app.post('/opinions',function(req,res)
 {Opinion.create(req.body).then(function(opinions){
   Opinion.findById(opinions.id).then(function(opinions){
       res.status(201).send(opinions);
   });
 });
});

app.get('/opinions/:id',function(req,res){
    Opinion.findById(req.params.id).then(function(opinions){
        if(opinions){
            res.status(200).send(opinions);
        }
        else{
            res.status(404).send();
        }
    })
});

app.put('/opinions/:id',function(req,res){
    Opinion.findById(req.params.id).then(function(opinions){
        if(opinions){
            opinions.updateAttributes(req.body).then(function(){
                res.status(200).send('updated');
            }).catch(function(error){
                console.warn(error);
                res.status(500).send('server error');
            });
        }else{
            res.status(404).send();
        }
    });
});

app.delete('/opinions/:id',function(req,res){
    Opinion.findById(req.params.id).then(function(opinions)
    {if(opinions){
        opinions.destroy().then(function(){
            res.status(204).send();
        }).catch(function(error){
            res.status(500).send('server error');
        });
    }else{
        res.status(404).send();
    }
  });
});

app.use(express.static('views'));
app.listen(process.env.PORT);

