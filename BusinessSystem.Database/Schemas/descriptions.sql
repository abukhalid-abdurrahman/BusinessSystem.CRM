-- Table: public.descriptions

-- DROP TABLE public.descriptions;

CREATE TABLE public.descriptions
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    description text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_descriptions PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.descriptions
    OWNER to postgres;