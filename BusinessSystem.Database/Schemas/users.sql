-- Table: public.users

-- DROP TABLE public.users;

CREATE TABLE public.users
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    role_id integer NOT NULL,
    image_id integer NOT NULL,
    description_id integer NOT NULL,
    login character varying(20) COLLATE pg_catalog."default" NOT NULL,
    phonenumber character varying(15) COLLATE pg_catalog."default" NULL,
    email character varying(40) COLLATE pg_catalog."default" NULL,
    password character varying(60) COLLATE pg_catalog."default" NOT NULL,
    username character varying(20) COLLATE pg_catalog."default" NOT NULL,
    removed boolean NOT NULL DEFAULT false,
    insertdate date NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT pk_users PRIMARY KEY (id),
    CONSTRAINT fk_users_descriptions FOREIGN KEY (description_id)
        REFERENCES public.descriptions (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_users_images FOREIGN KEY (image_id)
        REFERENCES public.images (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_users_roles FOREIGN KEY (role_id)
        REFERENCES public.roles (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.users
    OWNER to postgres;