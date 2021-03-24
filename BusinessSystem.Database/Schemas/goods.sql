-- Table: public.goods

-- DROP TABLE public.goods;

CREATE TABLE public.goods
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    user_id integer NOT NULL,
    image_id integer NOT NULL,
    description_id integer NOT NULL,
    category_id integer NOT NULL,
    name character varying(30) COLLATE pg_catalog."default" NOT NULL,
    removed boolean NOT NULL DEFAULT false,
    insertdate date NOT NULL DEFAULT CURRENT_DATE,
    CONSTRAINT pk_goods PRIMARY KEY (id),
    CONSTRAINT fk_goods_categories FOREIGN KEY (category_id)
        REFERENCES public.categories (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_goods_descriptions FOREIGN KEY (description_id)
        REFERENCES public.descriptions (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_goods_images FOREIGN KEY (image_id)
        REFERENCES public.images (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_goods_users FOREIGN KEY (user_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE public.goods
    OWNER to postgres;