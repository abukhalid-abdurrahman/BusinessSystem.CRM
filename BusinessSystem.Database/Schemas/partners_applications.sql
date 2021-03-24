CREATE TABLE public.partners_applications
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
	sender_id integer NOT NULL,
    recipient_id integer NOT NULL,
    application_text text,
    confirmed boolean NOT NULL DEFAULT false,
    confirmdate date,
    unconfirmdate date,
    insertdate date NOT NULL DEFAULT CURRENT_DATE,
    removed boolean NOT NULL DEFAULT false,
    CONSTRAINT pk_partners_applications PRIMARY KEY (id),
    CONSTRAINT fk_sender_id_users FOREIGN KEY (sender_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID,
    CONSTRAINT fk_recipient_id_users FOREIGN KEY (recipient_id)
        REFERENCES public.users (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        NOT VALID
);

ALTER TABLE public.partners_applications
    OWNER to postgres;