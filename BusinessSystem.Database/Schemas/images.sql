-- Table: public.images

-- DROP TABLE public.images;

CREATE TABLE public.images
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    filename character varying(100) COLLATE pg_catalog."default" NOT NULL,
    insertdate date NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT pk_images PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE public.images
    OWNER to postgres;