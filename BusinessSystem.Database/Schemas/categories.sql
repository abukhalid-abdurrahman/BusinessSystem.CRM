-- Table: public.categories

-- DROP TABLE public.categories;

CREATE TABLE public.categories
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    user_id integer NOT NULL,
    name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    removed boolean NOT NULL DEFAULT false,
    insertdate date NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT pk_categories PRIMARY KEY (id),
    CONSTRAINT fk_categories_users FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.categories
    OWNER to postgres;