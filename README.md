# WorkData.QueueScheduling
队列调入任务程序

WorkData.QueueScheduling是一个队列度任务程序，现支持立即执行，定时回调执行依赖于
redis rabbitmq nancy topself autofac castle dapper polly 及workdata基础设施
Redis 提供阻塞队列功能
rabbitmq 提供延迟队列功能
nancy 提供http请求用于项目解耦
topself 提供widows service宿主服务
workdata workdata框架提供的基础IOC注入功能（依赖于autofac）
autofac 提供ioc
dapper orm
castle.core 支持aop
polly 弹性和瞬态故障处理库
