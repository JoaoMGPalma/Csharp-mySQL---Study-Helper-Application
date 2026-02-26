create database if not exists db_quiz_estudo;
use db_quiz_estudo;

drop table if exists t_resultados_testes;
drop table if exists t_historico_testes;
drop table if exists t_respostas;
drop table if exists t_perguntas;
drop table if exists t_temas;
drop table if exists t_users;

create table t_users (
	id int primary key auto_increment,
    user_name varchar(50) unique not null,
    user_pass varchar(50) not null,
    data_inscricao datetime default current_timestamp,
    acesso enum("Admin", "Guest")
);

insert into t_users (user_name, user_pass, acesso)
values ("root", "123", "Admin");
insert into t_users (user_name, user_pass, acesso)
values ("joao", "123", "Guest");

-- select * from t_users;
-- Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' from t_users;

create table t_temas(
		id int primary key auto_increment,
        nome_tema varchar(50),
        descricao text
);

create table t_perguntas(
		id int primary key auto_increment,
        id_tema int,
        pergunta varchar(100),
        explicacao text,
        foreign key (id_tema) references t_temas(id)
);


create table t_respostas(
		id int primary key auto_increment,
        id_pergunta int,
        resposta varchar(100),
        correta bool,
        foreign key (id_pergunta) references t_perguntas(id)
);


create table t_historico_testes(
		id int primary key auto_increment,
        id_user int,
        tema varchar(50),
        resultado int,
        data_resultado datetime default current_timestamp
);
 
create table t_resultados_testes(
		id int primary key auto_increment,
        id_resultado int,
        id_pergunta int,
        id_resposta int,
        correta bool,
        foreign key (id_resultado) references t_historico_testes(id)
);        

-- select * from t_resultados_testes;     
-- select * from t_historico_testes;   